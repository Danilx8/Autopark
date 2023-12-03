﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace Autopark.Models
{
    public class Enterprise
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? City { get; set; }
        [ValidateNever]
        public List<Vehicle> Vehicles { get; set; } = new();
        [ValidateNever]
        public List<Driver> Drivers { get; set; } = new();
    }
}
