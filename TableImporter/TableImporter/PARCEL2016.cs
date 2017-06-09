namespace TableImporter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class PARCEL2016
    {
        [Key]
        [StringLength(100)]
        public string Acct { get; set; }

        [StringLength(256)]
        public string RecAcs { get; set; }

        [StringLength(256)]
        public string Shape_STAr { get; set; }

        [StringLength(256)]
        public string Shape_STLe { get; set; }

        public DbGeography Geography { get; set; }
    }
}
