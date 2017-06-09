namespace EF_Fix_Test
{
    using System.Data.Entity.Migrations;
    using DAL;

    public class MigrationConfiguration : DbMigrationsConfiguration<MyContext>
    {
        public MigrationConfiguration()
        {
            this.AutomaticMigrationsEnabled = false;
            AutomaticMigrationDataLossAllowed = true;
            MigrationsDirectory = @"MzCentralDbMigrations";
            CommandTimeout = 180;
        }

        protected override void Seed(MyContext context)
        {
            context.As.Add(new A()
            {
                Id = 5,
                Name = "Test Name"
            });
            base.Seed(context);
        }
    }
}