using System;
using System.IO;

namespace GeoJson2Sql
{
    class Program
    {
        static void Main(string[] args)
        {
            //var fileName = "PARCEL2016.geojson";
            //var folderPath = @"C:\Users\abdel\Downloads\PARCEL2016\PARCEL2016\";
            Console.WriteLine("Enter File Path: ");
            var filePath = Console.ReadLine();

            Console.WriteLine("Enter DB Connection String: ");
            var cnn = Console.ReadLine();
            var fileInfo = new FileInfo(filePath);
            var tableName = fileInfo.Name.Substring(0, fileInfo.Name.LastIndexOf("."));
            GeographyHelper.ProcessGeoJsonData(tableName, fileInfo.DirectoryName, fileInfo.Name, DateTime.Now.ToFileTime(), cnn);
        }
    }
}
