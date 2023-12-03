using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Autopark.Models
{
    public class Driver
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Salary { get; set; }
        public int EnterpriseId { get; set; }
        [ValidateNever]
        public Enterprise Enterprise { get; set; } = null!;
        public Vehicle? Vehicle { get; set; }
        [ValidateNever]
        public List<Vehicle> AssignedCars { get; set; } = new();
    }
}
