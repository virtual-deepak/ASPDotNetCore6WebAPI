namespace DotNetCoreWebAPI.Repository
{
    public interface ICityRepository
    {
        public Task<Entities.City> GetCityAsync(
            int cityId,
            bool includePointsOfInterest = false);
        public Task<IEnumerable<Entities.City>> GetCitiesAsync(
            string? name,
            string? searchText,
            bool includePointsOfInterest = false);
    }
}