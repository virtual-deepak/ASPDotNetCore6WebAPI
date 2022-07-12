using AutoMapper;

namespace DotNetCoreWebAPI.Profiles
{
    public class CityProfile : Profile
    {
        public CityProfile()
        {
            CreateMap<Entities.City, Models.CityDto>();
        }
    }
}