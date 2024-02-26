using NetTopologySuite.Geometries;

namespace Autopark.Models
{
    public class RidesVisualInfo
    {
        public required Point CenterCoordinates { get; set; }
        public required List<RideRenderInfo> Rides { get; set; }
    }
}
