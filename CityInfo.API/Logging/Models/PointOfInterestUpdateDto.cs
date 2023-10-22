using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Logging.Models;

public class PointOfInterestUpdateDto
{
    [Required(ErrorMessage = "You should provide a name.")]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(200)]
    public string? Description { get; set; }
}
