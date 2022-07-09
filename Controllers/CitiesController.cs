using DotNetCoreWebAPI.InMemoryDataStore;
using DotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly CitiesDataStore citiesDataStore;

        public CitiesController(CitiesDataStore citiesDataStore)
        {
            this.citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(this.citiesDataStore.Cities);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCity(int id)
        {
            var city = this.citiesDataStore.Cities?.FirstOrDefault(x => x.Id == id);
            return city == null ? NotFound() : Ok(city);
        }
    }
}