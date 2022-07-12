using AutoMapper;

namespace DotNetCoreWebAPI.Profiles
{
    public class PointOfInterest : Profile
    {
        public PointOfInterest()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
        }
    }
}