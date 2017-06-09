namespace TableImporter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class census_tract_DB
    {
        [Key]
        [StringLength(50)]
        public string tract { get; set; }

        [StringLength(50)]
        public string population { get; set; }

        [Column("food stamps - total")]
        [StringLength(50)]
        public string food_stamps___total { get; set; }

        [Column("food stamps")]
        [StringLength(50)]
        public string food_stamps { get; set; }

        [Column("Race Total")]
        [StringLength(50)]
        public string Race_Total { get; set; }

        [Column("# White")]
        [StringLength(50)]
        public string C__White { get; set; }

        [Column("# Black")]
        [StringLength(50)]
        public string C__Black { get; set; }

        [Column("# Hispanic-total")]
        [StringLength(50)]
        public string C__Hispanic_total { get; set; }

        [Column("# Hispanic")]
        [StringLength(50)]
        public string C__Hispanic { get; set; }

        [Column("Single Moms-total")]
        [StringLength(50)]
        public string Single_Moms_total { get; set; }

        [Column("Single Moms")]
        [StringLength(50)]
        public string Single_Moms { get; set; }

        [Column("Median Age")]
        [StringLength(50)]
        public string Median_Age { get; set; }

        [Column("Poverty-total")]
        [StringLength(50)]
        public string Poverty_total { get; set; }

        [Column("Income In Past 12 Months Below Poverty Level")]
        [StringLength(50)]
        public string Income_In_Past_12_Months_Below_Poverty_Level { get; set; }

        [Column("Total Housing Units")]
        [StringLength(50)]
        public string Total_Housing_Units { get; set; }

        [Column("Public Transit-total")]
        [StringLength(50)]
        public string Public_Transit_total { get; set; }

        [Column("Public Transit")]
        [StringLength(50)]
        public string Public_Transit { get; set; }

        [Column("Median Household Income")]
        [StringLength(50)]
        public string Median_Household_Income { get; set; }

        [Column("employment-total")]
        [StringLength(50)]
        public string employment_total { get; set; }

        [StringLength(50)]
        public string Unemployed { get; set; }

        [Column("Not In Labor Force")]
        [StringLength(50)]
        public string Not_In_Labor_Force { get; set; }

        [Column("Vacant-Total")]
        [StringLength(50)]
        public string Vacant_Total { get; set; }

        [StringLength(50)]
        public string Vacant { get; set; }

        [Column("travel time-total")]
        [StringLength(50)]
        public string travel_time_total { get; set; }

        [Column("45-59 minutes to work")]
        [StringLength(50)]
        public string C45_59_minutes_to_work { get; set; }

        [Column("60-89")]
        [StringLength(50)]
        public string C60_89 { get; set; }

        [Column("90+ minutes to work")]
        [StringLength(50)]
        public string C90__minutes_to_work { get; set; }

        [Column("Owner Occupied-total")]
        [StringLength(50)]
        public string Owner_Occupied_total { get; set; }

        [Column("Owner Occupied")]
        [StringLength(50)]
        public string Owner_Occupied { get; set; }

        [Column("Renter Occupied")]
        [StringLength(50)]
        public string Renter_Occupied { get; set; }

        [Column("Median Monthly Housing Cost")]
        [StringLength(50)]
        public string Median_Monthly_Housing_Cost { get; set; }

        [Column("Moved From Abroad-total")]
        [StringLength(50)]
        public string Moved_From_Abroad_total { get; set; }

        [Column("Moved From Abroad-Past Year")]
        [StringLength(50)]
        public string Moved_From_Abroad_Past_Year { get; set; }

        [Column("Foreign Born-Total")]
        [StringLength(50)]
        public string Foreign_Born_Total { get; set; }

        [Column("Foreign Born")]
        [StringLength(50)]
        public string Foreign_Born { get; set; }

        [Column("EduAttainment-total")]
        [StringLength(50)]
        public string EduAttainment_total { get; set; }

        [Column("HS diploma")]
        [StringLength(50)]
        public string HS_diploma { get; set; }

        [StringLength(50)]
        public string GED { get; set; }

        [Column("Some College-1 year or less")]
        [StringLength(50)]
        public string Some_College_1_year_or_less { get; set; }

        [Column("Some College-more than 1 year")]
        [StringLength(50)]
        public string Some_College_more_than_1_year { get; set; }

        [StringLength(50)]
        public string Associates { get; set; }

        [StringLength(50)]
        public string Bachelors { get; set; }

        [StringLength(50)]
        public string Masters { get; set; }

        [Column("Professional School Degree")]
        [StringLength(50)]
        public string Professional_School_Degree { get; set; }

        [Column("Doctorate Degree")]
        [StringLength(50)]
        public string Doctorate_Degree { get; set; }

        [Column("Industry-total")]
        [StringLength(50)]
        public string Industry_total { get; set; }

        [Column("Industry-manufacturing")]
        [StringLength(50)]
        public string Industry_manufacturing { get; set; }

        [Column("Column 47")]
        [StringLength(50)]
        public string Column_47 { get; set; }
    }
}
