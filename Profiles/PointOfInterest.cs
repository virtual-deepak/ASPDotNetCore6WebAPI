using AutoMapper;

namespace DotNetCoreWebAPI.Profiles
{
    public class PointOfInterest : Profile
    {
        public PointOfInterest()
        {
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();
            CreateMap<Models.PointOfInterestToCreateDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestToUpdateDto, Entities.PointOfInterest>();
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestToUpdateDto>();
        }
    }
}