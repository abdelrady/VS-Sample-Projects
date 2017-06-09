namespace TableImporter
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class census_zip_code
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public double ZipCode { get; set; }

        public double? Population { get; set; }

        public double? FoodStampsTotal { get; set; }

        public double? FoodStamps { get; set; }

        public double? PercentReceivingFoodStamps { get; set; }

        public double? RaceTotal { get; set; }

        public double? NumberWhite { get; set; }

        public double? PercentWhite { get; set; }

        public double? NumberAfricanAmerican { get; set; }

        public double? PercentAfricanAmerican { get; set; }

        public double? NumberHispanicTotal { get; set; }

        public double? NumberHispanic { get; set; }

        public double? PercentHispanic { get; set; }

        public double? SingleMomsTotal { get; set; }

        public double? SingleMoms { get; set; }

        public double? PercentSingleMoms { get; set; }

        public double? MedianAge { get; set; }

        public double? PovertyToal { get; set; }

        public double? IncomeInPast12MonthsBelowPovertyLevel { get; set; }

        public double? PercentIncomeBelowPovertyLevel { get; set; }

        public double? TotalHousingUnits { get; set; }

        public double? PublicTransitTotal { get; set; }

        public double? PublicTransit { get; set; }

        [StringLength(255)]
        public string F24 { get; set; }

        public double? MedianHouseholdIncome { get; set; }

        public double? EmploymentTotal { get; set; }

        public double? Unemployed { get; set; }

        public double? PercentUnemployed { get; set; }

        public double? NotInLaborForce { get; set; }

        public double? PercentNotInLaborForce { get; set; }

        public double? VacantTotal { get; set; }

        public double? Vacant { get; set; }

        public double? PercentVacant { get; set; }

        public double? TravelTimeTotal { get; set; }

        [Column("90PlusMinutesToWork")]
        public double? C90PlusMinutesToWork { get; set; }

        public double? Percent90PlusMinutesToWork { get; set; }

        public double? OwnerOccupiedTotal { get; set; }

        public double? OwnerOccupied { get; set; }

        public double? PercentOwnerOccupied { get; set; }

        public double? RenterOccupied { get; set; }

        public double? PercentRenterOccupied { get; set; }

        public double? MedianMonthlyHousingCost { get; set; }

        [StringLength(255)]
        public string MovedFromAbroad { get; set; }

        [StringLength(255)]
        public string MovedFromAbroadPastYear { get; set; }

        [StringLength(255)]
        public string PercentMovedFromAbroadPastYear { get; set; }

        public double? ForeignBornTotal { get; set; }

        public double? ForeignBorn { get; set; }

        public double? PercentForeignBorn { get; set; }

        public double? EduAttainmentTotal { get; set; }

        public double? HSDiploma { get; set; }

        public double? GED { get; set; }

        public double? SomeCollege1YearOrLess { get; set; }

        public double? SomeCollegeMoreThan1Year { get; set; }

        public double? Associates { get; set; }

        public double? Bachelors { get; set; }

        public double? Masters { get; set; }

        public double? ProfessionalSchoolDegree { get; set; }

        public double? DoctorateDegree { get; set; }

        public double? PercentHSGradOrAbove { get; set; }

        public double? IndustryTotal { get; set; }

        public double? IndustryManufacturing { get; set; }
    }
}
