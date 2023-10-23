using AddingControllers.DataStore;
using AddingControllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace AddingControllers.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/cities")]
[ApiVersion("1.0")]
[ApiVersion("2.0")]
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
