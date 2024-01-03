using Autopark.Models;
using Autopark.Models.Roles;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Autopark.Data
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Enterprise> Enterprises { get; set; }
        public DbSet<Driver> Drivers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            Brand chery = new() { Id = 1, Name = "Chery", RequiredDriverCategory = Brand.Categories.A, SeatsAmount = 5, Segment = Brand.Segments.DAILY, Type = Brand.Types.SUV };
            Brand kamaz = new() { Id = 2, Name = "Kamaz", RequiredDriverCategory = Brand.Categories.E, SeatsAmount = 2, Segment = Brand.Segments.LEASING, Type = Brand.Types.TRUCK };
            Brand ferrari = new() { Id = 3, Name = "Ferrari", RequiredDriverCategory = Brand.Categories.A, SeatsAmount = 2, Segment = Brand.Segments.BUSINESS, Type = Brand.Types.SPORT_CAR };

            Vehicle chery8 = new() { Id = 1, BrandId = 1, HorsePower = 5000, Mileage = 2000, Name = "Chery Tiggo Pro Max 8", Price = 3_120_000, Year = 2023, ZeroToHundred = 8.5, EnterpriseId = 1, DriverId = 1 };
            Vehicle chery7 = new() { Id = 2, BrandId = 1, HorsePower = 5500, Mileage = 4000, Name = "Chery Tiggo Pro Max 7", Price = 2_055_000, Year = 2022, ZeroToHundred = 9.8, EnterpriseId = 1 };
            Vehicle chery6 = new() { Id = 3, BrandId = 1, HorsePower = 5000, Mileage = 10_000, Name = "Chery Tiggo Pro Max 6", Price = 3_120_000, Year = 2021, ZeroToHundred = 8.5, EnterpriseId = 2, DriverId = 2 };
            Vehicle kamazVehicle = new() { Id = 4, BrandId = 2, HorsePower = 2500, Mileage = 14_000, Name = "КАМАЗ 69592", Price = 3_055_000, Year = 2012, ZeroToHundred = 26, EnterpriseId = 2 };
            Vehicle ferrariVehicle = new() { Id = 5, BrandId = 3, HorsePower = 10_000, Mileage = 0, Name = "FERRARI F430", Price = 130_120_000, Year = 2020, ZeroToHundred = 1.8, EnterpriseId = 3, DriverId = 3 };

            Driver vasyl = new() { Id = 1, Name = "Василий", Salary = 45_000, EnterpriseId = 1 };
            Driver gena = new() { Id = 2, Name = "Геннадий", Salary = 30_000, EnterpriseId = 2 };
            Driver jenya = new() { Id = 3, Name = "Евгений", Salary = 50_000, EnterpriseId = 3 };
            Driver sanya = new() { Id = 4, Name = "Александр", Salary = 55_000, EnterpriseId = 1 };
            Driver grisha = new() { Id = 5, Name = "Григорий", Salary = 35_000, EnterpriseId = 2 };

            Enterprise mgt = new() { Id = 1, Name = "Мосгортранс", City = "Москва" };
            Enterprise dhl = new() { Id = 2, Name = "DHL", City = "Мюнхен" };
            Enterprise st = new() { Id = 3, Name = "Сибтранс", City = "Новосибирск" };

            Manager manager = new() { Id = "1" };

            var hasher = new PasswordHasher<AppUser>();
            AppUser sam = new()
            {
                Id = "1",
                UserName = "Manager Sam",
                NormalizedUserName = "SAM",
                PasswordHash = hasher.HashPassword(null, "qwerty")
            };
            AppUser tom = new()
            {
                Id = "2",
                UserName = "Manager Tom",
                NormalizedUserName = "TOM",
                PasswordHash = hasher.HashPassword(null, "qwerty")
            };

            modelBuilder.Entity<Driver>()
                .HasOne(d => d.Vehicle)
                .WithOne(v => v.Driver)
                .HasForeignKey<Vehicle>(v => v.DriverId)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Driver>()
                .HasMany(d => d.AssignedCars)
                .WithMany(v => v.AssignedDrivers)
                .UsingEntity<Dictionary<string, object>>(
                    "VehicleDriver",
                    v => v.HasOne<Vehicle>().WithMany().HasForeignKey("AssignedCarsId"),
                    d => d.HasOne<Driver>().WithMany().HasForeignKey("AssignedDriversId"),
                    vd =>
                    {
                        vd.HasKey("AssignedDriversId", "AssignedCarsId");
                        vd.HasData(
                            new { AssignedCarsId = 1, AssignedDriversId = 1 },
                            new { AssignedCarsId = 1, AssignedDriversId = 2 },
                            new { AssignedCarsId = 2, AssignedDriversId = 3 },
                            new { AssignedCarsId = 2, AssignedDriversId = 4 },
                            new { AssignedCarsId = 6, AssignedDriversId = 5 },
                            new { AssignedCarsId = 4, AssignedDriversId = 1 },
                            new { AssignedCarsId = 4, AssignedDriversId = 2 },
                            new { AssignedCarsId = 5, AssignedDriversId = 3 },
                            new { AssignedCarsId = 5, AssignedDriversId = 4 });
                    });

            modelBuilder.Entity<Manager>().HasData(manager);

            modelBuilder.Entity<AppUser>().HasData(sam, tom);

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>()
                {
                    RoleId = manager.Id,
                    UserId = sam.Id
                },
                new IdentityUserRole<string>()
                {
                    RoleId = manager.Id,
                    UserId = tom.Id
                });

            modelBuilder.Entity<EnterpriseManager>().HasData(
                new EnterpriseManager
                {
                    UserId = "1",
                    ManagedEnterpriseId = 1
                },
                new EnterpriseManager
                {
                    UserId = "1",
                    ManagedEnterpriseId = 2
                },
                new EnterpriseManager
                {
                    UserId = "2",
                    ManagedEnterpriseId = 2
                },
                new EnterpriseManager
                {
                    UserId = "2",
                    ManagedEnterpriseId = 3
                });

            modelBuilder.Entity<Vehicle>().HasData(chery8, chery7, chery6, kamazVehicle, ferrariVehicle);

            modelBuilder.Entity<Brand>().HasData(chery, kamaz, ferrari);

            modelBuilder.Entity<Driver>().HasData(vasyl, gena, jenya, sanya, grisha);

            modelBuilder.Entity<Enterprise>().HasData(mgt, dhl, st);
        }
        public DbSet<Autopark.Models.Roles.Manager> Manager { get; set; } = default!;
    }
}
