using System.Collections.Generic;
using DotNetCoreWebAPI.InMemoryDataStore;

namespace DotNetCoreWebAPI.Models
{
    public class CityDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }

        public int? NumPointsOfInterest => this.PointsOfInterest.Count;

        public ICollection<PointOfInterestDto> PointsOfInterest { get; set; }
            = new List<PointOfInterestDto>();
    }
}