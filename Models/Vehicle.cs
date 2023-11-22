using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autopark.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public double Price { get; set; }        
        public double ZeroToHundred { get; set; }
        public int Mileage { get; set; }
        public int Year { get; set; }
        public int HorsePower { get; set; }
        public int BrandId { get; set; }
        [ForeignKey("BrandId")]
        [ValidateNever]
        public Brand Brand { get; set; } = null!;
    }
}
