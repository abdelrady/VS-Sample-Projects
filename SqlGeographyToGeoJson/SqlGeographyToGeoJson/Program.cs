using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Spatial;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace SqlGeographyToGeoJson
{
    class Program
    {
        static void Main(string[] args)
        {
            var context = new CensusTractsContext();
            var tract = context.CensusTracts.First();

            var camelCaseSerialize = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            var geoJsonObject = new GeoJsonResult
            {
                Features =
                    new List<GeographyResult>() {new GeographyResult {Geometry = tract.Geography, Type = "Feature"}}
            };

            var geojson = JsonConvert.SerializeObject(geoJsonObject, camelCaseSerialize);

            File.WriteAllText("D:\\geojson_test.txt", geojson);
        }

    }

    
    //Entity Model
    [Table("census_tracts")]
    public class CensusTract
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string GeoId { get; set; }
        [JsonConverter(typeof(DbGeographyGeoJsonConverter))]
        public DbGeography Geography { get; set; }
    }

    //DbContext
    public class CensusTractsContext : DbContext
    {
        public CensusTractsContext()
            : base("CensusTractsContext")
        {
            this.Database.CommandTimeout = 240;
            Database.SetInitializer<CensusTractsContext>(null);
        }

        public DbSet<CensusTract> CensusTracts { get; set; }
    }


    //ViewModels
    public class GeoJsonResult
    {
        public string Type => "FeatureCollection";
        public List<GeographyResult> Features { get; set; }
    }

    public class GeographyResult
    {
        [JsonConverter(typeof(DbGeographyGeoJsonConverter))]
        public DbGeography Geometry { get; set; }

        public string Type { get; set; }
    }

}
