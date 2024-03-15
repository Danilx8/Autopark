namespace Autopark.Models.Reports
{
    public class CarMileage : Report
    {
        public required int CarId { get; set; } 
        public required Dictionary<DateOnly, float> Mileage { get; set; }
    }
}
