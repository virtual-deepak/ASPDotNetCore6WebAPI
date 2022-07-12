using System.Net;
using AutoMapper;
using DotNetCoreWebAPI.Helpers;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Repository;
using DotNetCoreWebAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [Route("api/cities/{cityId:int}/pointsofinterest")]
    public class PointOfInterestController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly IPointOfInterestRepository pointOfInterestRepository;
        private readonly ICityRepository cityRepository;
        private readonly IMapper mapper;
        private readonly ILogger<PointOfInterestController> _logger;
        public PointOfInterestController(
            ILogger<PointOfInterestController> logger,
            IMailService mailService,
            IPointOfInterestRepository pointOfInterestRepository,
            ICityRepository cityRepository,
            IMapper mapper)
        {
            this._mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            this.pointOfInterestRepository = pointOfInterestRepository ?? throw new ArgumentNullException(nameof(pointOfInterestRepository));
            this.cityRepository = cityRepository ?? throw new ArgumentNullException(nameof(cityRepository));
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this._logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        public async Task<IActionResult> GetPointsOfInterest(int cityId)
        {
            var city = await cityRepository.GetCityAsync(cityId);
            if (city == null)
            {
                _logger.LogInformation($"Cannot find city with id: {cityId}");
                return NotFound();
            }

            var pointsOfInterest = await pointOfInterestRepository.GetPointsOfInterestAsync(cityId);
            return Ok(mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterest));
        }

        [HttpGet("{id:int}", Name = "GetPointOfInterest")]
        public async Task<IActionResult> GetPointOfInterest(int cityId, int id)
        {
            try
            {
                var city = await this.cityRepository.GetCityAsync(cityId);
                if (city == null)
                    return NotFound();

                var pointOfInterest = await this.pointOfInterestRepository.GetPointOfInterestAsync(
                    cityId,
                    id
                );
                if (pointOfInterest == null)
                    return NotFound();

                return Ok(this.mapper.Map<PointOfInterestDto>(pointOfInterest));
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception occurred while calling GetPointOfInterest",
                    ex);
                return StatusCode((int)HttpStatusCode.InternalServerError, "An error occurred on server");
            }
        }

        // [HttpPost]
        // public IActionResult CreatePointOfInterest(int cityId,
        //     PointOfInterestToCreateDto pointOfInterest)
        // {
        //     var city = HelperFunctions.GetCity(cityId);
        //     if (city == null)
        //         return NotFound();

        //     int maxCityId = city.PointsOfInterest.Max(x => x.Id);
        //     var newPointOfInterest = new PointOfInterestDto()
        //     {
        //         Id = ++maxCityId,
        //         Description = pointOfInterest.Description,
        //         Name = pointOfInterest.Name
        //     };

        //     city.PointsOfInterest.Add(newPointOfInterest);

        //     return CreatedAtRoute("GetPointOfInterest",
        //         new
        //         {
        //             cityId,
        //             id = newPointOfInterest.Id
        //         },
        //         newPointOfInterest);
        // }

        // [HttpPut("{id:int}")]
        // public IActionResult UpdatePointOfInterest(int cityId,
        //     int id,
        //     PointOfInterestToUpdateDto pointOfInterest)
        // {
        //     var existingPointOfInterest = HelperFunctions.GetExistingPointOfInterest(cityId, id);
        //     if (existingPointOfInterest == null)
        //         return NotFound();

        //     existingPointOfInterest.Description = pointOfInterest.Description;
        //     existingPointOfInterest.Name = pointOfInterest.Name;

        //     return NoContent();
        // }

        // [HttpPatch("{id:int}")]
        // public IActionResult PatchPointOfInterest(
        //     int cityId,
        //     int id,
        //     [FromBody] JsonPatchDocument<PointOfInterestToUpdateDto> jsonPatchModel)
        // {
        //     var city = HelperFunctions.GetCity(cityId);
        //     if (city == null)
        //     {
        //         return NotFound();
        //     }

        //     var existingPointOfInterest = HelperFunctions.GetExistingPointOfInterest(cityId, id);
        //     if (existingPointOfInterest == null)
        //     {
        //         return NotFound();
        //     }

        //     var pointOfInterestToUpdateDto = new PointOfInterestToUpdateDto()
        //     {
        //         Description = existingPointOfInterest.Description,
        //         Name = existingPointOfInterest.Name
        //     };

        //     // Input model inputs applied to existing entity. 
        //     // Any errors will be populated into current ModelState i.e. JsonPatchDocument<PointOfInterestToUpdateDto>
        //     jsonPatchModel.ApplyTo(pointOfInterestToUpdateDto, ModelState);

        //     // Check ModelState validity explicitly as JsonPatch uses Newtonsoft.Json
        //     // and not out of the box Json formatters which is inferred by ApiController automatically
        //     // based on annotations. 
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     // Validate if the patched entity is valid or not based on annotations applied
        //     // as ModelState here is JsonPatchDocument<PointOfInterestToUpdateDto>
        //     // and not PointOfInterestToUpdateDto
        //     // Any errors however, will continue to populate in ModelState.
        //     if (!TryValidateModel(pointOfInterestToUpdateDto))
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     existingPointOfInterest.Description = pointOfInterestToUpdateDto.Description;
        //     existingPointOfInterest.Name = pointOfInterestToUpdateDto.Name;

        //     return NoContent();
        // }

        // [HttpDelete("{id:int}")]
        // public IActionResult DeletePointOfInterest(
        //     int cityId,
        //     int id)
        // {
        //     var city = HelperFunctions.GetCity(cityId);
        //     if (city == null)
        //     {
        //         return NotFound();
        //     }

        //     var existingPointOfInterest = HelperFunctions.GetExistingPointOfInterest(cityId, id);
        //     if (existingPointOfInterest == null)
        //     {
        //         return NotFound();
        //     }

        //     city.PointsOfInterest.Remove(existingPointOfInterest);
        //     this._mailService.SendMail(
        //         $"Point of interest deleted.",
        //         $"Point of interest {existingPointOfInterest.Name} with id {existingPointOfInterest.Id}");
        //     return NoContent();
        // }
    }
}