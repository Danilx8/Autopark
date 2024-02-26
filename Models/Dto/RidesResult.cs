namespace Autopark.Models.Dto
{
    public class RidesResult
    {
        public int RideId { get; set; }
        public int VehicleId { get; set; }
        public DateTime StartTime { get; set; }
        public string StartLocation { get; set; } = "";
        public DateTime FinishTime { get; set; }
        public string FinishLocation { get; set; } = "";
    }
}
