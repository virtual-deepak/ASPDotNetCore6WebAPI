using System.ComponentModel.DataAnnotations;

namespace DotNetCoreWebAPI.Models
{
    public class PointOfInterestToCreateDto
    {
        [Required(ErrorMessage = "Name field is required")]
        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(200)]
        public string? Description { get; set; }
    }
}