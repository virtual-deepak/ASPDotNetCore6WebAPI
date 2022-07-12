using DotNetCoreWebAPI.Models;

namespace DotNetCoreWebAPI.Repository
{
    public interface ICityRepository
    {
        public Task<Entities.City> GetCityAsync(
            int cityId,
            bool includePointsOfInterest = false);
        public Task<(IEnumerable<Entities.City>, PaginationMetadata)> GetCitiesAsync(
            string? name,
            string? searchText,
            int pageNumber,
            int pageSize,
            bool includePointsOfInterest = false);
    }
}