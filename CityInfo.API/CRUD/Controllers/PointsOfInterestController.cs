using AddingControllers.DataStore;
using AddingControllers.Models;
using CRUD.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace AddingControllers.Controllers;

[ApiController]
[Route("api/cities/{cityId}/pointsofinterest")]
public class PointsOfInterestController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(
        int cityId)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        if (cityDto == null)
            return NotFound();
        return Ok(cityDto.PointsOfInterest);
    }

    [HttpGet("{id}", Name = "GetPointOfInterest")]
    public ActionResult<IEnumerable<PointOfInterestDto>> GetPointOfInterest(
        int cityId, int id)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        if (cityDto == null)
            return NotFound();
        
        var pointOfInterestDto = cityDto.PointsOfInterest.FirstOrDefault(x => x.Id == id);
        if (pointOfInterestDto == null)
            return NotFound();
        
        return Ok(pointOfInterestDto);
    }

    [HttpPost]
    public ActionResult CreatePointOfInterest(
        int cityId, 
        PointOfInterestCreateDto pointOfInterest)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        if (cityDto == null)
            return NotFound();
        
        var maxId = 
            CitiesDataStore.Current.Cities.SelectMany(x => x.PointsOfInterest).Max(x => x.Id);
        
        var finalPOI = new PointOfInterestDto()
        {
            Id = ++maxId,   
            Description = pointOfInterest.Description,
            Name = pointOfInterest.Name
        };
        cityDto.PointsOfInterest.Add(finalPOI);
        return CreatedAtRoute("GetPointOfInterest", new { cityId, finalPOI.Id }, finalPOI);
    }

    [HttpPut("{id}")]
    public ActionResult CreatePointOfInterest(
        int cityId, 
        int id,
        PointOfInterestUpdateDto pointOfInterest)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        if (cityDto == null)
            return NotFound();

        var pointOfInterestDto = cityDto.PointsOfInterest.FirstOrDefault(x => x.Id == id);
        if (pointOfInterestDto == null)
            return NotFound();

        pointOfInterestDto.Name = pointOfInterest.Name;
        pointOfInterestDto.Description = pointOfInterest.Description;

        return NoContent();
    }

    [HttpPatch("{id}")]
    public ActionResult PatchPointOfInterest(
        int cityId,
        int id,
        JsonPatchDocument<PointOfInterestUpdateDto> patchDocument)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        if (cityDto == null)
            return NotFound();

        var pointOfInterestInStore = cityDto.PointsOfInterest.FirstOrDefault(x => x.Id == id);
        if (pointOfInterestInStore == null)
            return NotFound();

        var pointOfInterestToPatch = new PointOfInterestUpdateDto()
        {
            Description = pointOfInterestInStore.Description,
            Name = pointOfInterestInStore.Name
        };

        // Passing in the ModelState stores any errors during applying the patch
        patchDocument.ApplyTo(pointOfInterestToPatch, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        // The ModelState here is JsonPatchDocument<PointOfInterestUpdateDto>
        // We need to check whether applying the patch to PointOfInterestUpdateDto model itself
        // holds valid based on annotations applied for its validity.
        if (!TryValidateModel(pointOfInterestToPatch))
            return BadRequest(ModelState);

        pointOfInterestInStore.Name = pointOfInterestToPatch.Name;
        pointOfInterestInStore.Description = pointOfInterestToPatch.Description;
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public ActionResult DeletePointOfInterest(int cityId, int id)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == cityId);
        if (cityDto == null)
            return NotFound();

        var pointOfInterestInStore = cityDto.PointsOfInterest.FirstOrDefault(x => x.Id == id);
        if (pointOfInterestInStore == null)
            return NotFound();
        
        cityDto.PointsOfInterest.Remove(pointOfInterestInStore);
        return NoContent();
    }
}
