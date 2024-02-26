using System.ComponentModel.DataAnnotations;

namespace Autopark.Models
{
    public class Ride
    {
        [Key]
        public int Id { get; set; }
        public required DateTime Start { get; set; }
        public required DateTime Finish { get; set; }
        public required int VehicleId { get; set; }
    }
}
