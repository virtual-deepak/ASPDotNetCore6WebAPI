namespace DotNetCoreWebAPI.Repository
{
    public interface ICityRepository
    {
        public Task<Entities.City> GetCityAsync(int cityId, bool includePointsOfInterest = false);
        public Task<IEnumerable<Entities.City>> GetCitiesAsync(bool includePointsOfInterest = false);
    }
}