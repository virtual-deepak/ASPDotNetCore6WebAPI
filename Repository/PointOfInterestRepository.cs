using DotNetCoreWebAPI.DbContexts;
using DotNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Repository
{
    public class PointOfInterestRepository : IPointOfInterestRepository
    {
        private readonly CityInfoDbContext context;

        public PointOfInterestRepository(
            CityInfoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<PointOfInterest> CreateAsync(PointOfInterest pointOfInterest)
        {
            this.context.PointOfInterest.Add(pointOfInterest);
            await this.context.SaveChangesAsync();
            return await this.GetPointOfInterestAsync(pointOfInterest.Id);
        }

        public async Task DeleteAsync(int pointOfInterestId)
        {
            var pointOfInterest = await this.GetPointOfInterestAsync(pointOfInterestId);
            this.context.PointOfInterest.Remove(pointOfInterest);
            await this.context.SaveChangesAsync();
        }

        public async Task<PointOfInterest> GetPointOfInterestAsync(
            int pointOfInterestId)
        {
            return await this.context.PointOfInterest
                .FirstOrDefaultAsync(x => x.Id == pointOfInterestId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestAsync(int cityId)
        {
            return await this.context.PointOfInterest
                .Where(x => x.CityId == cityId)
                .ToListAsync();
        }

        public async Task UpdateAsync(PointOfInterest pointOfInterest)
        {
            this.context.PointOfInterest.Update(pointOfInterest);
            await this.context.SaveChangesAsync();
        }
    }
}