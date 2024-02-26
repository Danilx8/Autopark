using NetTopologySuite.Geometries;

namespace Autopark.Models
{
    public class RideRenderInfo
    {  
        public required Ride Ride { get; set; }
        public required LineString Path { get; set; }
        public string Color { get; set; } = "#000000";
    }
}
