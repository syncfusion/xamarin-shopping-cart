using System.Data.Entity.Migrations;
using ShoppingAPI.Database;

namespace ShoppingAPI.Migrations
{
    /// <summary>
    /// This class is responsible for the Configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<ShoppingContext>
    {
        /// <summary>
        /// Initializes the configuration.
        /// </summary>
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        /// <summary>
        /// Seed method.
        /// </summary>
        /// <param name="context"></param>
        protected override void Seed(ShoppingContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }
    }
}