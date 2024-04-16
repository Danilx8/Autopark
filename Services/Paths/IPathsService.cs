using Autopark.Models;

namespace Autopark.Services.Paths
{
    public interface IPathsService
    {
        public Geopoint? FindExactPoint(int vehicleId, DateTime time);
        public List<Geopoint>? ReadAllPoints(int vehicleId, DateTime? start, DateTime? finish, int limit = 1);
        public List<Ride>? ReadAllRides(int vehicleId, DateTime? start, DateTime? finish, int limit = 1);
    }
}
