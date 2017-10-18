using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Entities
{
    public class CityInfoContext : DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> options) : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate(); // Does the same as above; creates if doesn't exist. But also handles migrations. Must start with no db to apply migrations. So delete existing db if there is one.
            // Before this, make sure to run in the package manager console: Add-Migration CityInfoDbInitialMigration
        }

        // A DbSet lets you query an entity type
        public DbSet<City> Cities { get; set; }
        public DbSet<PointOfInterest> PointsOfInterest { get; set; }

        // One way of configuring db
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("connectionString");

        //    base.OnConfiguring(optionsBuilder);
        //}
    }    
}
