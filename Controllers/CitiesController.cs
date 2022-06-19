using DotNetCoreWebAPI.InMemoryDataStore;
using DotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetCities()
        {
            return Ok(new CitiesDataStore());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetCity(int id)
        {
            var city = new CitiesDataStore().Cities?.FirstOrDefault(x => x.Id == id);
            return city == null ? NotFound() : Ok(city);
        }
    }
}