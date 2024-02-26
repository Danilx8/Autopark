using Autopark.Data;
using Autopark.Models;

namespace Autopark.Services.Paths
{
    public class PathService(ApplicationDbContext _db) : IPathsService
    {
        protected readonly ApplicationDbContext db = _db;

        public List<Geopoint> ReadAllPoints(int vehicleId, DateTime? start, DateTime? finish, int limit = 1000)
        {
            if (start == null || finish == null)
            {
                return [.. db.Points
                    .Where(p => p.VehicleId == vehicleId)
                    .OrderBy(p => p.RegisterTime)
                    .Take(limit)];
            }
            return [.. db.Points
                        .Where(p => p.VehicleId == vehicleId
                            && p.RegisterTime >= start
                            && p.RegisterTime <= finish)
                        .OrderBy(p => p.RegisterTime)
                        .Take(limit)];
        }
    }
}
