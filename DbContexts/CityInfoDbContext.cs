using DotNetCoreWebAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreWebAPI.DbContexts
{
    public class CityInfoDbContext : DbContext
    {
        public CityInfoDbContext(DbContextOptions<CityInfoDbContext> dbContextOptions)
            : base(dbContextOptions)
        {

        }

        public DbSet<City> City { get; set; }

        public DbSet<PointOfInterest> PointOfInterest { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City()
                {
                    Id = 1,
                    Name = "New York City",
                    Description = "The one with that big park."
                },
                new City()
                {
                    Id = 2,
                    Name = "Antwerp",
                    Description = "The one with the cathedral that was never really finished.",
                },
                new City()
                {
                    Id = 3,
                    Name = "Paris",
                    Description = "The one with that big tower.",
                }
            );
            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest()
                {
                    Id = 1,
                    Name = "Central Park",
                    Description = "The most visited urban park in the United States.",
                    CityId = 1,
                },
                new PointOfInterest()
                {
                    Id = 2,
                    Name = "Empire State Building",
                    Description = "A 102-story skyscraper located in Midtown Manhattan.",
                    CityId = 1
                },
                new PointOfInterest()
                {
                    Id = 3,
                    Name = "Cathedral of Our Lady",
                    Description = "A Gothic style cathedral, conceived by architects Jan and Pieter Appelmans.",
                    CityId = 2
                },
                new PointOfInterest()
                {
                    Id = 4,
                    Name = "Antwerp Central Station",
                    Description = "The the finest example of railway architecture in Belgium.",
                    CityId = 2
                },
                new PointOfInterest()
                {
                    Id = 5,
                    Name = "Eiffel Tower",
                    Description = "A wrought iron lattice tower on the Champ de Mars, named after engineer Gustave Eiffel.",
                    CityId = 3
                },
                new PointOfInterest()
                {
                    Id = 6,
                    Name = "The Louvre",
                    Description = "The world's largest museum.",
                    CityId = 3
                }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}