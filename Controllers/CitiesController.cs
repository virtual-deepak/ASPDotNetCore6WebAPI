using System.Text.Json;
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
        private readonly IConfiguration configuration;

        public CitiesController(
            ICityRepository cityRepository,
            IMapper mapper,
            IConfiguration configuration)
        {
            this.cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet]
        public async Task<IActionResult> GetCities(
            string? name,
            string? searchText,
            int? pageSize,
            int pageNumber = 1,
            bool includePointsOfInterest = false)
        {
            int maxPageSize = Convert.ToInt32(configuration["ApiSettings:MaxPageSize"]);
            pageSize = pageSize > maxPageSize || (pageSize == null) ? maxPageSize : pageSize;

            var (cities, paginationMetadata) = await this.cityRepository.GetCitiesAsync(
                name,
                searchText,
                pageNumber,
                pageSize.Value,
                includePointsOfInterest);

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return Ok(this.mapper.Map<IEnumerable<CityDto>>(cities));
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