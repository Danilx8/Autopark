using Autopark.Models;

namespace Autopark.Services.Paths
{
    public interface IPathsService
    {
        public List<Geopoint> ReadAllPoints(int vehicleId, DateTime? start, DateTime? finish, int limit = 1000);
        
    }
}
