using System.Collections.Generic;
using DotNetCoreWebAPI.DbContexts;
using DotNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.Repository
{
    public class CityRepository : ICityRepository
    {
        private readonly CityInfoDbContext context;
        public CityRepository(CityInfoDbContext context)
        {
            this.context = context ?? throw new ArgumentNullException(nameof(context));

        }
        public async Task<IEnumerable<City>> GetCitiesAsync(
            string? name,
            string? searchText,
            bool includePointsOfInterest = false)
        {
            IQueryable<City> citiesQueryable = this.context.City;
            if (!string.IsNullOrEmpty(name))
            {
                citiesQueryable = citiesQueryable.Where(x => x.Name == name);
            }

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                citiesQueryable = citiesQueryable.Where(x => x.Name.Contains(searchText)
                    || (x.Description != null && x.Description.Contains(searchText)));
            }

            if (includePointsOfInterest)
            {
                return await citiesQueryable
                    .Include(x => x.PointsOfInterest)
                    .ToListAsync();
            }
            return await citiesQueryable.ToListAsync();
        }

        public async Task<City> GetCityAsync(
            int cityId,
            bool includePointsOfInterest = false)
        {
            if (includePointsOfInterest)
            {
                return await this.context.City
                    .Include(x => x.PointsOfInterest)
                    .FirstOrDefaultAsync(x => x.Id == cityId);
            }
            return await this.context.City
                .FirstOrDefaultAsync(x => x.Id == cityId);
        }
    }
}