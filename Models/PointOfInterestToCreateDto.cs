namespace DotNetCoreWebAPI.Models
{
    public class PointOfInterestToCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}