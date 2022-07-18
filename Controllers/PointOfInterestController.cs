using System.Net;
using AutoMapper;
using DotNetCoreWebAPI.Models;
using DotNetCoreWebAPI.Repository;
using DotNetCoreWebAPI.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DotNetCoreWebAPI.Controllers
{
    [ApiController]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/cities/{cityId:int}/pointsofinterest")]
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

                var pointOfInterest = await this.pointOfInterestRepository.GetPointOfInterestAsync(id);

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

        [HttpPost]
        public async Task<IActionResult> CreatePointOfInterest(int cityId,
            PointOfInterestToCreateDto pointOfInterest)
        {
            var city = await this.cityRepository.GetCityAsync(cityId);
            if (city == null)
                return NotFound();

            var entity = this.mapper.Map<Entities.PointOfInterest>(pointOfInterest);
            entity.CityId = cityId;
            entity = await this.pointOfInterestRepository.CreateAsync(entity);

            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId,
                    id = entity.Id
                },
                this.mapper.Map<PointOfInterestDto>(entity));
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdatePointOfInterest(int cityId,
            int id,
            PointOfInterestToUpdateDto pointOfInterest)
        {
            var city = await this.cityRepository.GetCityAsync(cityId);
            if (city == null)
                return NotFound();

            var inStorePointOfInterest = await this.pointOfInterestRepository.GetPointOfInterestAsync(id);
            if (inStorePointOfInterest == null)
                return NotFound();

            this.mapper.Map(pointOfInterest, inStorePointOfInterest);
            await this.pointOfInterestRepository.UpdateAsync(inStorePointOfInterest);

            return NoContent();
        }

        [HttpPatch("{id:int}")]
        public async Task<IActionResult> PatchPointOfInterest(
            int cityId,
            int id,
            [FromBody] JsonPatchDocument<PointOfInterestToUpdateDto> jsonPatchModel)
        {
            var city = await this.cityRepository.GetCityAsync(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var inStorePointOfInterest = await this.pointOfInterestRepository.GetPointOfInterestAsync(id);
            if (inStorePointOfInterest == null)
                return NotFound();

            var pointOfInterestToPatch = this.mapper.Map<PointOfInterestToUpdateDto>(inStorePointOfInterest);

            // Input model inputs applied to existing entity. 
            // Any errors will be populated into current ModelState i.e. JsonPatchDocument<PointOfInterestToUpdateDto>
            jsonPatchModel.ApplyTo(pointOfInterestToPatch, ModelState);

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
            if (!TryValidateModel(pointOfInterestToPatch))
            {
                return BadRequest(ModelState);
            }

            this.mapper.Map(pointOfInterestToPatch, inStorePointOfInterest);
            await this.pointOfInterestRepository.UpdateAsync(inStorePointOfInterest);
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeletePointOfInterest(
            int cityId,
            int id)
        {
            var city = await this.cityRepository.GetCityAsync(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var inStorePointOfInterest = await this.pointOfInterestRepository.GetPointOfInterestAsync(id);
            if (inStorePointOfInterest == null)
                return NotFound();

            await this.pointOfInterestRepository.DeleteAsync(id);

            this._mailService.SendMail(
                $"Point of interest deleted.",
                $"Point of interest {inStorePointOfInterest.Name} with id {inStorePointOfInterest.Id}");
            return NoContent();
        }
    }
}