namespace EYM.Presentation.Admin.Migrations
{
	using System.Data.Entity.Migrations;
	using EntityFramework;

	internal sealed class Configuration : DbMigrationsConfiguration<EYMContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

		protected override void Seed(EYMContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
			//context.People.AddOrUpdate(
			//  p => p.FullName,
			//  new Person { FullName = "Andrew Peters" },
			//  new Person { FullName = "Brice Lambson" },
			//  new Person { FullName = "Rowan Miller" }
			//);
            //
        }
    }
}
