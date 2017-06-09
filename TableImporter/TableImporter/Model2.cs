namespace TableImporter
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Model2 : DbContext
    {
        public Model2()
            : base("name=Model2")
        {
        }

        public virtual DbSet<census_tract_DB> census_tract_DB { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.tract)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.population)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.food_stamps___total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.food_stamps)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Race_Total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C__White)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C__Black)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C__Hispanic_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C__Hispanic)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Single_Moms_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Single_Moms)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Median_Age)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Poverty_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Income_In_Past_12_Months_Below_Poverty_Level)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Total_Housing_Units)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Public_Transit_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Public_Transit)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Median_Household_Income)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.employment_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Unemployed)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Not_In_Labor_Force)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Vacant_Total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Vacant)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.travel_time_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C45_59_minutes_to_work)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C60_89)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.C90__minutes_to_work)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Owner_Occupied_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Owner_Occupied)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Renter_Occupied)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Median_Monthly_Housing_Cost)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Moved_From_Abroad_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Moved_From_Abroad_Past_Year)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Foreign_Born_Total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Foreign_Born)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.EduAttainment_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.HS_diploma)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.GED)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Some_College_1_year_or_less)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Some_College_more_than_1_year)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Associates)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Bachelors)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Masters)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Professional_School_Degree)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Doctorate_Degree)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Industry_total)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Industry_manufacturing)
                .IsUnicode(false);

            modelBuilder.Entity<census_tract_DB>()
                .Property(e => e.Column_47)
                .IsUnicode(false);
        }
    }
}
