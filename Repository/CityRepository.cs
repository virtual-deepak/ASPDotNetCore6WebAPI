using DotNetCoreWebAPI.DbContexts;
using DotNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly CityInfoDbContext context;
        public CityRepository(CityInfoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public async Task<IEnumerable<City>> GetCitiesAsync(bool includePointsOfInterest = false)
        {
            if (includePointsOfInterest)
            {
                return await this.context.City.Include(x => x.PointsOfInterest)
                    .ToListAsync();
            }
            return await this.context.City.ToListAsync();
        }

        public async Task<City> GetCityAsync(
            int cityId,
            bool includePointsOfInterest = false)
        {
            if (includePointsOfInterest)
            {
                return await this.context.City
                    .Include(x => x.PointsOfInterest)
                    .FirstOrDefaultAsync(x => x.Id == cityId);
            }
            return await this.context.City
                .FirstOrDefaultAsync(x => x.Id == cityId);
        }
    }
}