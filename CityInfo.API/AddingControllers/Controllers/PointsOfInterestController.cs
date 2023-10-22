using AddingControllers.DataStore;
using AddingControllers.Models;
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

    [HttpGet("{id}")]
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
}
