using CityInfo.API.Logging.DataStore;
using CityInfo.API.Logging.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Logging.Controllers;

[ApiController]
[Route("api/cities")]
// [Route("api/[controller]")] -- This is also an option
public class CitiesController : ControllerBase
{
    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return Ok(CitiesDataStore.Current.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var cityDto = CitiesDataStore.Current.Cities.FirstOrDefault(x => x.Id == id);
        if (cityDto == null)
            return NotFound();
        return Ok(cityDto);
    }
}
