using Autopark.Models;
using System.Runtime.CompilerServices;

namespace Autopark.Dto
{
    public class VehicleDto
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Price { get; set; }
        public float ZeroToHundred { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int BrandId { get; set; }
        public int? DriverId { get; set; }
        public int? EnterpriseId { get; set; }
        public IEnumerable<int> AssignedDrivers { get; set; } = new List<int>();

        public VehicleDto() { }

        public VehicleDto(Vehicle vehicle)
        {
            if (vehicle is null) 
            {
                return;
            }

            List<int> assignedDriversIds = new();
            foreach(Driver driver in vehicle.AssignedDrivers) {
                assignedDriversIds.Add(driver.Id);
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
            EnterpriseId    = vehicle?.Enterprise?.Id;
            AssignedDrivers = assignedDriversIds;
        }
    }
}
