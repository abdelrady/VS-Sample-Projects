namespace TableImporter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DART_routes
    {
        [StringLength(256)]
        public string trip_headsign { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int shape_id { get; set; }

        public int? direction_id { get; set; }

        public int? route_id { get; set; }

        [StringLength(256)]
        public string route_short_name { get; set; }

        [StringLength(256)]
        public string route_long_name { get; set; }

        public DbGeography Geography { get; set; }
    }
}
