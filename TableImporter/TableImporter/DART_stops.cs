namespace TableImporter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class DART_stops
    {
        [StringLength(256)]
        public string stop_name { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int stop_id { get; set; }

        public DbGeography Geography { get; set; }
    }
}
