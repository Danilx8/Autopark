using Autopark.Models;
using System.Runtime.CompilerServices;

namespace Autopark.Dto
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }
        public double ZeroToHundred { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int BrandId { get; set; }
        public int? DriverId { get; set; }
        public IEnumerable<Driver> AssignedDrivers { get; set; } = new List<Driver>();

        public VehicleDto() { }

        public VehicleDto(Vehicle vehicle)
        {
            if (vehicle is null) 
            {
                return;
            }

            Id              = vehicle.Id;
            Name            = vehicle.Name;
            Price           = vehicle.Price;
            ZeroToHundred   = vehicle.ZeroToHundred;
            Mileage         = vehicle.Mileage;
            Year            = vehicle.Year;
            HorsePower      = vehicle.HorsePower;
            BrandId         = vehicle.BrandId;
            DriverId        = vehicle?.Driver?.Id;
            AssignedDrivers = vehicle.AssignedDrivers;
        }
    }
}
