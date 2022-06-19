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

        [HttpGet("{id:int}", Name = "GetPointOfInterest")]
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

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId,
            PointOfInterestToCreateDto pointOfInterest)
        {
            var city = GetCity(cityId);
            if (city == null)
                return NotFound();

            int maxCityId = city.PointsOfInterest.Max(x => x.Id);
            var newPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxCityId,
                Description = pointOfInterest.Description,
                Name = pointOfInterest.Name
            };

            city.PointsOfInterest.Add(newPointOfInterest);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    id = newPointOfInterest.Id
                },
                newPointOfInterest);
        }

        [HttpPut("{id}")]
        public IActionResult UpdatePointOfInterest(int cityId,
            int id,
            PointOfInterestToUpdateDto pointOfInterest)
        {
            var existingPointOfInterest = GetExistingPointOfInterest(cityId, id);
            if (existingPointOfInterest == null)
                return NotFound();

            existingPointOfInterest.Description = pointOfInterest.Description;
            existingPointOfInterest.Name = pointOfInterest.Name;

            return NoContent();
        }

        private CityDto GetCity(int cityId)
        {
            return CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        }

        private PointOfInterestDto GetExistingPointOfInterest(int cityId, int id)
        {
            return CitiesDataStore.Current.Cities
                .FirstOrDefault(x => x.Id == cityId)?
                .PointsOfInterest
                .FirstOrDefault(x => x.Id == id);
        }
    }
}