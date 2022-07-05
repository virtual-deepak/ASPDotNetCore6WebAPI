using DotNetCoreWebAPI.InMemoryDataStore;
using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Helpers
{
    public static class HelperFunctions
    {
        public static CityDto? GetCity(int cityId)
        {
            return CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        }

        public static PointOfInterestDto? GetExistingPointOfInterest(int cityId, int id)
        {
            return CitiesDataStore.Current.Cities
                .FirstOrDefault(x => x.Id == cityId)?
                .PointsOfInterest
                .FirstOrDefault(x => x.Id == id);
        }
    }
}