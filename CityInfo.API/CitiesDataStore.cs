using CityInfo.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API
{
    public class CitiesDataStore
    {
        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public List<CityDto> Cities { get; set; }

        public CitiesDataStore()
        {
            // Init dummy data
            Cities = new List<CityDto>()
            {
                new CityDto()
                {
                    Id = 1,
                    Name = "Chester",
                    Description = "Old Roman city",
                    PointsOfInterest = new List<PointOfInterestDto>()
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Amphitheatre",
                            Description = "Romans used to battle to the death here"
                        },
                        new PointOfInterestDto()
                        {
                            Id = 2,
                            Name = "Clock",
                            Description = "Romans used to read the time here"
                        }
                    }
                },
                new CityDto()
                {
                    Id = 2,
                    Name = "Liverpool",
                    Description = "Home of the Beetles and the Liverbird!"
                }
            };           
        }
    }
}
