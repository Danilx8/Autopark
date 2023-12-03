using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autopark.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public double Price { get; set; }        
        public double ZeroToHundred { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int BrandId { get; set; }
        [ValidateNever]
        public Brand Brand { get; set; } = null!;

        public int? EnterpriseId { get; set; }
        [ValidateNever]
        public Enterprise? Enterprise { get; set; }
        public int? DriverId { get; set; }
        [ValidateNever]
        public Driver? Driver { get; set; }
        public List<Driver> AssignedDrivers { get; set; } = new();
    }
}
