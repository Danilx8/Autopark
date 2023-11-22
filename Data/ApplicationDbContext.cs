using Autopark.Models;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Chery", RequiredDriverCategory = Brand.Categories.A, SeatsAmount = 5, Segment = Brand.Segments.DAILY, Type = Brand.Types.SUV },
                new Brand { Id = 2, Name = "Kamaz", RequiredDriverCategory = Brand.Categories.E, SeatsAmount = 2, Segment = Brand.Segments.LEASING, Type = Brand.Types.TRUCK },
                new Brand { Id = 3, Name = "Ferrari", RequiredDriverCategory = Brand.Categories.A, SeatsAmount = 2, Segment = Brand.Segments.BUSINESS, Type = Brand.Types.SPORT_CAR}
                );

            modelBuilder.Entity<Vehicle>().HasData(
                new Vehicle { Id = 1, BrandId = 1, HorsePower = 5000, Mileage = 2000, Name = "Chery Tiggo Pro Max 8", Price = 3_120_000, Year = 2023, ZeroToHundred = 8.5 },
                new Vehicle { Id = 2, BrandId = 1, HorsePower = 5500, Mileage = 4000, Name = "Chery Tiggo Pro Max 7", Price = 2_055_000, Year = 2022, ZeroToHundred = 9.8},
                new Vehicle { Id = 3, BrandId = 1, HorsePower = 5000, Mileage = 10_000, Name = "Chery Tiggo Pro Max 6", Price = 3_120_000, Year = 2021, ZeroToHundred = 8.5},
                new Vehicle { Id = 4, BrandId = 2, HorsePower = 2500, Mileage = 14_000, Name = "КАМАЗ 69592", Price = 3_055_000, Year = 2012, ZeroToHundred = 26 },
                new Vehicle { Id = 5, BrandId = 3, HorsePower = 10_000, Mileage = 0, Name = "FERRARI F430", Price = 130_120_000, Year = 2020, ZeroToHundred = 1.8 }
                );
        }
    }
}
