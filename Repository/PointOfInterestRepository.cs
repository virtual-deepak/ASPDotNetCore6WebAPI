using DotNetCoreWebAPI.DbContexts;
using DotNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Repository
{
    public class PointOfInterestRepository : IPointOfInterestRepository
    {
        private readonly CityInfoDbContext context;
        public PointOfInterestRepository(CityInfoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PointOfInterest> GetPointOfInterestAsync(
            int cityId,
            int pointOfInterestId)
        {
            return await this.context.PointOfInterest
                .FirstOrDefaultAsync(x => x.Id == pointOfInterestId
                    && x.CityId == cityId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId)
        {
            return await this.context.PointOfInterest
                .Where(x => x.CityId == cityId)
                .ToListAsync();
        }
    }
}