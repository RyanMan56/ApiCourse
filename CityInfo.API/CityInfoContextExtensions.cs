using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public static class CityInfoContextExtensions
    {
        public static void EnsureSeedDataForContext(this CityInfoContext context) // This keyword before a parameter defines an extension method. This means EnsureSeedDataForContext 
                                                                                  // extends the CityInfoContext class. So you can call CityInfoContext.EnsureSeedDataForContext()
        {
            if (context.Cities.Any())
            {
                return; // If there is any data, don't seed.
            }

            // init seed data
            var cities = new List<City>()
            {
                new City()
                {
                    Name = "Leeds",
                    Description = "Least Yorkshire bit of Yorkshire.",
                    PointsOfInterest = new List<PointOfInterest>()
                    {
                        new PointOfInterest()
                        {
                            Name = "Space",
                            Description = "The club, not the thing."
                        },
                        new PointOfInterest()
                        {
                            Name = "That park",
                            Description = "Pretty nice."
                        }
                    }
                },
                new City()
                {
                    Name = "London",
                    Description = "Capital of England",
                    PointsOfInterest = new List<PointOfInterest>()
                    {

                    }
                }
            };

            context.Cities.AddRange(cities); // tracks the data but doesn't save it
            context.SaveChanges(); // effectively, executes the statement
        }
    }
}
