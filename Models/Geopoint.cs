using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Autopark.Models
{
    public class Geopoint
    {
        [Key]
        public Guid UUID { get; set; } = Guid.NewGuid();
        [JsonConverter(typeof(PointConverter))]
        public required Point Point { get; set; }
        public required DateTime RegisterTime { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }

    }
}
