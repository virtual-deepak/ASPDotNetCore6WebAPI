using AutoMapper;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;
        public CitiesController(
            ICityRepository cityRepository,
            IMapper mapper)
        {
            this.cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(
            string? name,
            string? searchText,
            bool includePointsOfInterest = false)
        {
            return Ok(
                this.mapper.Map<IEnumerable<CityDto>>(
                    await this.cityRepository.GetCitiesAsync(name, searchText, includePointsOfInterest))
            );
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCity(
            int id,
            bool includePointsOfInterest = false)
        {
            var city = await this.cityRepository.GetCityAsync(id, includePointsOfInterest);
            if (city == null)
            {
                return NotFound();
            }

            return Ok(this.mapper.Map<CityDto>(city));
        }
    }
}