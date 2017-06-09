using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Migrations.Infrastructure;
using System.IO;
using System.Configuration;
using DAL;
using InteractivePreGeneratedViews;
using System;
using System.CodeDom.Compiler;
using System.Data.Entity.Core.Metadata.Edm;
namespace EF_Fix_Test
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(Path.GetTempPath());
			Console.WriteLine(string.Join(",", Enumerable.Empty<string>()));
			return;
			//Database.SetInitializer(new MigrateDatabaseToLatestVersion<DefaultConnection, MyContextConfiguration>());
			var random = new Random();
			Database.SetInitializer<MyContext>(null);
			using (var context = new MyContext())
			{
				Console.WriteLine(Path.Combine(ConfigurationManager.AppSettings["EF"], context.Database.Connection.Database + ".xml"));
				//InteractiveViews.SetViewCacheFactory(context,
				//    new FileViewCacheFactory(Path.Combine(ConfigurationManager.AppSettings["EF"], context.Database.Connection.Database + ".xml")));
				//InteractiveViews.SetViewCacheFactory(context,
				//new SqlServerViewCacheFactory(context.Database.Connection.ConnectionString));

				context.Database.CreateIfNotExists();
				DatabaseConfig.MigrateDatabase(context.Database.Connection.ConnectionString);
				/*var migrator = new DbMigrator(new MigrationConfiguration());
				var pendingMigrations = migrator.GetPendingMigrations();
				if (pendingMigrations.Any())
					migrator.Update();*/

				//context.As.Add(new A() { Name = "A" + random.Next() });
				//context.Bs.Add(new B() { Name = "B" + random.Next() });

				//context.SaveChanges();

				Console.WriteLine(context.As.Count());
				Console.WriteLine(context.Bs.Count());
			}
		}
	}
}
/*public class MyModelStore : DefaultDbModelStore
	{
		public MyModelStore(string location)
			: base(location)
		{
		}

		public override DbCompiledModel TryLoad(Type contextType)
		{
			// TODO: Return null if the model source code has changed:
			// entities, context, conventions, etc.
			// The basic idea is to perform the check without building
			// the model. That should work as long as the time it takes
			// to load the .edmx plus the time to perform the check is
			// less than the time to build the model.
			// Maybe the check can be done by looking at timestamps
			// on source files and comparing with the timestamp of the
			// .edmx file (GetFilePath(contextType))
			// Another teorethical option is to build the model source 
			// code into a netmodule or separate assembly and compare 
			// the timestamp with the .edmx timestamp.
			// It may also be acceptable to do nothing here, but put 
			// the model source code into a separate directory and have
			// a development process that requires deleting the existing
			// .edmx manually when something in that directory changes,
			// so it gets regenerated.
			// NOTE: I have only tried the third option, maybe there are
			// other possibilities.

			return base.TryLoad(contextType);
		}

		// This is not needed if using default schema "dbo"
		protected override string GetDefaultSchema(Type contextType)
		{
			return "MySchema";
		}
	}

	public class MyConfiguration : DbConfiguration
	{
		public MyConfiguration()
		{
			SetModelStore(new MyModelStore(@"C:\"));
		}
	}*/
