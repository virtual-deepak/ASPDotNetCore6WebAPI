using CityInfo.API.Services.DataStore;
using CityInfo.API.Services.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace CityInfo.API.Services.Controllers;

[ApiController]
[Route("api/cities")]
// [Route("api/[controller]")] -- This is also an option
public class CitiesController : ControllerBase
{
    public readonly IMailService _mailService;
    private readonly CitiesDataStore _citiesDataStore;


    public CitiesController(IMailService mailService,
        CitiesDataStore citiesDataStore)
    {
        _mailService = mailService;
        _citiesDataStore = citiesDataStore;

    }

    [HttpGet]
    public ActionResult<IEnumerable<CityDto>> GetCities()
    {
        return Ok(_citiesDataStore.Cities);
    }

    [HttpGet("{id}")]
    public ActionResult<CityDto> GetCity(int id)
    {
        var cityDto = _citiesDataStore.Cities.FirstOrDefault(x => x.Id == id);
        if (cityDto == null)
            return NotFound();
        _mailService.Send($"City: {id}", "Returned from the API");
        return Ok(cityDto);
    }
}
