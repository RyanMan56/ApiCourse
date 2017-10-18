using CityInfo.API.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CityInfo.API.Services
{
    public interface ICityInfoRepository
    {
        bool CityExists(int cityId);
        IEnumerable<City> GetCities(); // IEnumerable or IQueryable. IQueryable: consumer can keep building on, e.g. add OrderBy, Where, etc. But persistance leaked from repository, which seems to violate the
                                       // purpose of the repository. But writing a load of queries manually can get out of hand. Fairly straightforward API so IEnumerable used here.
        City GetCity(int id, bool includePointsOfInterest);
        IEnumerable<PointOfInterest> GetPointsOfInterestForCity(int cityId);
        PointOfInterest GetPointOfInterestForCity(int cityId, int pointOfInterestId);
        void AddPointOfInterestForCity(int cityId, PointOfInterest pointOfInterest);
        void DeletePointOfInterest(PointOfInterest pointOfInterest);
        bool Save();
    }
}
