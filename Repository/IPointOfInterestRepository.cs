namespace DotNetCoreWebAPI.Repository
{
    public interface IPointOfInterestRepository
    {
        public Task<Entities.PointOfInterest> GetPointOfInterestAsync(int pointOfInterestId);

        public Task<IEnumerable<Entities.PointOfInterest>> GetPointsOfInterestAsync(
            int cityId);

        public Task<Entities.PointOfInterest> CreateAsync(Entities.PointOfInterest pointOfInterest);

        public Task UpdateAsync(Entities.PointOfInterest pointOfInterest);

        public Task DeleteAsync(int pointOfInterestId);
    }
}