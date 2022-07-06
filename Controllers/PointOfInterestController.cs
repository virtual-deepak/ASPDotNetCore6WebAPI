using System.Net;
using DotNetCoreWebAPI.Helpers;
using DotNetCoreWebAPI.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId:int}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly ILogger<PointOfInterestController> _logger;
        public PointOfInterestController(
            ILogger<PointOfInterestController> logger,
            IHttpContextAccessor httpContextAccessor)
        {
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));

            // Alternate way to get injected services if not injected through controller
            // However, DI is preferred way
            // this._logger =
            // httpContextAccessor.HttpContext.RequestServices.GetService<ILogger<PointOfInterestController>>();
        }

        [HttpGet]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = HelperFunctions.GetCity(cityId);
            if (city == null)
            {
                _logger.LogInformation($"Cannot find city with id: {cityId}");
                return NotFound();
            }

            return Ok(city.PointsOfInterest);
        }

        [HttpGet("{id:int}", Name = "GetPointOfInterest")]
        public IActionResult GetPointOfInterest(int cityId, int id)
        {
            try
            {
                throw new Exception("Simulated exception...");
                var city = HelperFunctions.GetCity(cityId);
                if (city == null)
                    return NotFound();

                var pointOfInterest = city.PointsOfInterest.FirstOrDefault(x => x.Id == id);
                if (pointOfInterest == null)
                    return NotFound();

                return Ok(pointOfInterest);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occurred while calling GetPointOfInterest",
                    ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred on server");
            }
        }

        [HttpPost]
        public IActionResult CreatePointOfInterest(int cityId,
            PointOfInterestToCreateDto pointOfInterest)
        {
            var city = HelperFunctions.GetCity(cityId);
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
                    cityId,
                    id = newPointOfInterest.Id
                },
                newPointOfInterest);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdatePointOfInterest(int cityId,
            int id,
            PointOfInterestToUpdateDto pointOfInterest)
        {
            var existingPointOfInterest = HelperFunctions.GetExistingPointOfInterest(cityId, id);
            if (existingPointOfInterest == null)
                return NotFound();

            existingPointOfInterest.Description = pointOfInterest.Description;
            existingPointOfInterest.Name = pointOfInterest.Name;

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public IActionResult PatchPointOfInterest(
            int cityId,
            int id,
            [FromBody] JsonPatchDocument<PointOfInterestToUpdateDto> jsonPatchModel)
        {
            var city = HelperFunctions.GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var existingPointOfInterest = HelperFunctions.GetExistingPointOfInterest(cityId, id);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            var pointOfInterestToUpdateDto = new PointOfInterestToUpdateDto()
            {
                Description = existingPointOfInterest.Description,
                Name = existingPointOfInterest.Name
            };

            // Input model inputs applied to existing entity. 
            // Any errors will be populated into current ModelState i.e. JsonPatchDocument<PointOfInterestToUpdateDto>
            jsonPatchModel.ApplyTo(pointOfInterestToUpdateDto, ModelState);

            // Check ModelState validity explicitly as JsonPatch uses Newtonsoft.Json
            // and not out of the box Json formatters which is inferred by ApiController automatically
            // based on annotations. 
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validate if the patched entity is valid or not based on annotations applied
            // as ModelState here is JsonPatchDocument<PointOfInterestToUpdateDto>
            // and not PointOfInterestToUpdateDto
            // Any errors however, will continue to populate in ModelState.
            if (!TryValidateModel(pointOfInterestToUpdateDto))
            {
                return BadRequest(ModelState);
            }

            existingPointOfInterest.Description = pointOfInterestToUpdateDto.Description;
            existingPointOfInterest.Name = pointOfInterestToUpdateDto.Name;

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public IActionResult DeletePointOfInterest(
            int cityId,
            int id)
        {
            var city = HelperFunctions.GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var existingPointOfInterest = HelperFunctions.GetExistingPointOfInterest(cityId, id);
            if (existingPointOfInterest == null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(existingPointOfInterest);
            return NoContent();
        }
    }
}