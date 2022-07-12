namespace DotNetCoreWebAPI.Repository
{
    public interface IPointOfInterestRepository
    {
        public Task<Entities.PointOfInterest> GetPointOfInterestAsync(
            int cityId,
            int pointOfInterestId);

        public Task<IEnumerable<Entities.PointOfInterest>> GetPointsOfInterestAsync(
            int cityId);
    }
}