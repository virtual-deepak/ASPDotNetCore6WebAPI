using DotNetCoreWebAPI.InMemoryDataStore;
using DotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = GetCity(cityId);
            if (city == null)
                return NotFound();

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{id:int}")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            var city = GetCity(cityId);
            if (city == null)
                return NotFound();

            var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
            if (pointOfInterest == null)
                return NotFound();

            return Ok(pointOfInterest);
        }

        private CityDto GetCity(int cityId)
        {
            return new CitiesDataStore().Cities.FirstOrDefault(x => x.Id == cityId);
        }
    }
}