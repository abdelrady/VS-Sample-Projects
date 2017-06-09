using System;
using System.Runtime;
using System.Runtime.Serialization;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using GeoJSON.Net;
using GeoJSON.Net.Feature;
using GeoJSON.Net.Geometry;
using Microsoft.SqlServer.Types;
using Newtonsoft.Json;
using Point = GeoJSON.Net.Geometry.Point;

namespace GeoJson2Sql
{
    public class GeographyHelper
    {
        public static DbGeometry MakeValid(DbGeometry geom)
        {
            if (geom.IsValid)
                return geom;

            return DbGeometry.FromText(SqlGeometry.STGeomFromText(new SqlChars(geom.AsText()), 4326).MakeValid().STAsText().ToSqlString().ToString(), 4326);
        }

        public static DbGeography GeneratePolygonFromCoordinates(double latitude1, double longitude1, double latitude2,
            double longitude2)
        {
            var geomText = GeneratePolygonTextFromCoordinates(latitude1, longitude1, latitude2, longitude2);

            DbGeography polygon = DbGeography.PolygonFromText(geomText, DbGeography.DefaultCoordinateSystemId);
            //4326 = [WGS84]

            return polygon;
        }

        public static string GeneratePolygonTextFromCoordinates(double latitude1, double longitude1, double latitude2,
            double longitude2)
        {
            var locationText =
                $"polygon(({longitude1} {latitude1}, {longitude2} {latitude1}, {longitude2} {latitude2}, {longitude1} {latitude2}, {longitude1} {latitude1}))";

            var geometry =
                SqlGeometry.STGeomFromText(new SqlChars(locationText), DbGeography.DefaultCoordinateSystemId).MakeValid();

            var geomText = geometry.STAsText().ToSqlString().ToString();
            return geomText;
        }

        public static void ProcessGeoJsonData(string tableName, string folderPath, string fileName, long fileStamp, string cnnString)
        {
            SqlConnection cnn = null;
            SqlCommand cmd = null;
            if (!string.IsNullOrEmpty(cnnString))
            {
                cnn = new SqlConnection(cnnString);
                cnn.Open();
                cmd = new SqlCommand {Connection = cnn};
            }

            var firstRecord = true;
            var columnsList = new List<string>();

            var file = folderPath + "\\" + tableName + "_UpdateStatements" + fileStamp + ".sql";
            if (!File.Exists(file)) File.Create(file).Close();

            var sw = new StreamWriter(file);

            Console.WriteLine("Reading File");
            var geoJsonText = File.ReadAllText(folderPath + "\\" + fileName);
            Console.WriteLine("Done Reading File");

            Console.WriteLine("De-serializing File");

            var featureCollection = JsonConvert.DeserializeObject<FeatureCollection>(geoJsonText);

            Console.WriteLine("Done De-serializing File");
            
            Console.WriteLine("Processing File");

            foreach (var feature in featureCollection.Features)
            {
                var updateStatment =
                "insert into " + tableName + " values(";

                string geography = null;
                if (feature.Geometry != null)
                {
                    var sb = new StringBuilder();
                    ConvertGeometryObject(feature.Geometry, sb);

                    var geoJson = sb.ToString();
                    if (!string.IsNullOrEmpty(geoJson))
                    {
                        geography = GetValidGeography(geoJson).WellKnownValue.WellKnownText;
                    }
                }

                foreach (var property in feature.Properties)
                {
                    if (firstRecord)
                    {
                        if (!columnsList.Contains(property.Key))
                            columnsList.Add(property.Key);
                    }

                    if (columnsList.Contains(property.Key))
                        updateStatment += $"'{property.Value}', ";

                }

                if (firstRecord)
                {
                    firstRecord = false;
                    File.WriteAllText(folderPath + "\\" + tableName + "_schema" + fileStamp + ".txt", string.Join(",", columnsList));
                }

                updateStatment += !string.IsNullOrEmpty(geography) ? $"geography::STGeomFromText('{geography}', 4326)" : "null";
                updateStatment += ")";
                if (cnn != null)
                {
                    cmd.CommandText = updateStatment;
                    cmd.ExecuteNonQuery();
                }
                else sw.WriteLine(updateStatment);
            }
            Console.WriteLine("Done Processing File");
            cnn?.Close();
            sw.Flush();
            sw.Close();
            sw.Dispose();
        }

        public static DbGeography GetValidGeography(DbGeography geography)
        {
            if (geography.SpatialTypeName.ToLower() == "GEOMETRYCOLLECTION".ToLower())
            {
                var sb = new StringBuilder();
                var serializedFeature = JsonConvert.SerializeObject(geography, new DbGeographyGeoJsonConverter());

                var collection = JsonConvert.DeserializeObject<GeometryCollection>(serializedFeature);
                if (collection != null)
                {
                    var firstLoop = true;
                    sb.Append("GEOMETRYCOLLECTION (");
                    foreach (var geometryObject in collection.Geometries)
                    {
                        if (!firstLoop)
                            sb.Append(",");
                        ConvertGeometryObject(geometryObject, sb);
                        firstLoop = false;
                    }
                    sb.Append(")");
                }
                return DbGeography.FromText(sb.ToString());
            }
            return GetValidGeography(geography.WellKnownValue.WellKnownText);
        }

        public static DbGeography GetValidGeography(string wktString)
        {
            var sqlGeography =
            SqlGeography.STGeomFromText(new SqlChars(wktString), DbGeography.DefaultCoordinateSystemId)
            .MakeValid();

            var invertedSqlGeography = sqlGeography.ReorientObject();
            if (sqlGeography.STArea() > invertedSqlGeography.STArea())
                sqlGeography = invertedSqlGeography;

            return DbSpatialServices.Default.GeographyFromProviderValue(sqlGeography);
        }

        public static void ConvertGeometryObject(IGeometryObject geometry, StringBuilder sb)
        {
            if (geometry.Type == GeoJSONObjectType.Point)
            {
                var point = geometry as Point;
                if (point != null)
                {
                    sb.Append("point ");
                    AddPointCoordinates(point, sb);
                }
            }
            else if (geometry.Type == GeoJSONObjectType.MultiPoint)
            {
                var multiPoint = geometry as MultiPoint;
                if (multiPoint != null)
                {
                    sb.Append("MULTIPOINT (");
                    foreach (var point in multiPoint.Coordinates)
                    {
                        AddPointCoordinates(point, sb);
                    }
                    sb.Append(")");
                }
            }
            else if (geometry.Type == GeoJSONObjectType.LineString)
            {
                var lineString = geometry as LineString;
                sb.Append("LINESTRING ");
                AddLineStringCoordinates(sb, lineString);
            }
            else if (geometry.Type == GeoJSONObjectType.Polygon)
            {
                var sbPolygon = new StringBuilder();
                sbPolygon.Append("polygon (");
                var polygon = geometry as Polygon;
                if (polygon != null)
                    AddPolygonCoordinates(polygon, sbPolygon);
                sbPolygon.Append(")");
                var validPolygon = GetValidGeography(sbPolygon.ToString());
                sb.Append(validPolygon.WellKnownValue.WellKnownText);
            }
            else if (geometry.Type == GeoJSONObjectType.MultiPolygon)
            {
                var multiPolygon = geometry as MultiPolygon;
                if (multiPolygon != null)
                {
                    var firstLoop = true;
                    sb.Append("MULTIPOLYGON (");
                    foreach (var polygon in multiPolygon.Coordinates)
                    {
                        if (!firstLoop)
                            sb.Append(",");
                        sb.Append("(");
                        AddPolygonCoordinates(polygon, sb);
                        sb.Append(")");
                        firstLoop = false;
                    }
                }
                sb.Append(")");
            }
            else if (geometry.Type == GeoJSONObjectType.GeometryCollection)
            {
                var collection = geometry as GeometryCollection;
                if (collection != null)
                {
                    var firstLoop = true;
                    sb.Append("GEOMETRYCOLLECTION (");
                    foreach (var geometryObject in collection.Geometries)
                    {
                        if (!firstLoop)
                            sb.Append(",");
                        ConvertGeometryObject(geometryObject, sb);
                        firstLoop = false;
                    }
                    sb.Append(")");
                }
            }
        }

        private static void AddPointCoordinates(Point point, StringBuilder sb)
        {
            var coord = point?.Coordinates as GeographicPosition;
            if (coord != null)
                sb.Append("(" + coord.Longitude + " " + coord.Latitude + ")");
        }

        private static void AddLineStringCoordinates(StringBuilder sb, LineString lineString)
        {
            sb.Append("(");
            var lst = new List<string>();
            foreach (var position in lineString.Coordinates)
            {
                var pos = position as GeographicPosition;
                lst.Add(pos.Longitude + " " + pos.Latitude);
            }
            lst.Reverse();
            sb.Append(string.Join(",", lst));
            sb.Append(")");
        }

        private static void AddPolygonCoordinates(Polygon polygon, StringBuilder sb)
        {
            var firstLoop = true;
            foreach (var lineString in polygon.Coordinates)
            {
                if (!firstLoop)
                    sb.Append(",");
                AddLineStringCoordinates(sb, lineString);
                firstLoop = false;
            }
        }

        public static string ConvertTopoToGeoJson(string phantomjsPath, string topoJson)
        {
            try
            {
                var tempFolder = Path.GetTempPath();
                var destinationFile = DateTime.Now.ToFileTime();
                string filePath = $"{tempFolder}\\{destinationFile}.txt",
                    outputFilePath = $"{tempFolder}\\{destinationFile}_out.txt";

                File.WriteAllText(filePath, topoJson);

                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = "\"" + phantomjsPath + "\\phantomjs.exe\"";
                startInfo.Arguments = $" {phantomjsPath}\\examples\\PhJS_Topo2Geo.js {filePath} {outputFilePath}";
                startInfo.CreateNoWindow = false;
                startInfo.UseShellExecute = false;
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                startInfo.RedirectStandardOutput = true;

                var phProcess = Process.Start(startInfo);
                phProcess.WaitForExit();

                var geoJsonString = File.ReadAllText(outputFilePath);

                return geoJsonString;
            }
            catch (Exception e)
            {
            }
            return string.Empty;
        }

    }
}
