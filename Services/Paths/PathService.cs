using Autopark.Data;
using Autopark.Models;
using Microsoft.Extensions.Caching.Memory;

namespace Autopark.Services.Paths
{
    public class PathService(ApplicationDbContext db, IMemoryCache cache) : IPathsService
    {
        public Geopoint? FindExactPoint(int vehicleId, DateTime time)
        {
            if (cache.TryGetValue((vehicleId, time), out Geopoint? point)) return point;

            var uncachedPoint = db.Points
                .FirstOrDefault(p => p.VehicleId == vehicleId
                                     && p.RegisterTime.Date == time.Date
                                     && p.RegisterTime.Hour == time.Hour
                                     && p.RegisterTime.Minute == time.Minute
                                     && p.RegisterTime.Second == time.Second);
            cache.Set((vehicleId, time), uncachedPoint);
            return uncachedPoint;
        }

        public List<Geopoint>? ReadAllPoints(int vehicleId, DateTime? start, DateTime? finish, int limit = 1)
        {
            if (cache.TryGetValue((vehicleId, start, finish), out List<Geopoint>? points)) return points;

            List<Geopoint> uncachedPoints;
            if (start == null || finish == null)
            {
                uncachedPoints = [.. db.Points
                    .Where(p => p.VehicleId == vehicleId)
                    .OrderBy(p => p.RegisterTime)
                    .Take(limit)];

                cache.Set((vehicleId, start, finish), uncachedPoints);
                return uncachedPoints;
            }
            
            uncachedPoints = [.. db.Points
                        .Where(p => p.VehicleId == vehicleId
                            && p.RegisterTime >= start
                            && p.RegisterTime <= finish)
                        .OrderBy(p => p.RegisterTime)
                        .Take(limit)];
            cache.Set((vehicleId, start, finish), uncachedPoints);
            return uncachedPoints;
        }

        public List<Ride>? ReadAllRides(int vehicleId, DateTime? start, DateTime? finish, int limit = 1)
        {
            if (cache.TryGetValue((vehicleId, start, finish), out List<Ride>? rides)) return rides;

            List<Ride> uncachedRides;
            if (start == null || finish == null)
            {
                uncachedRides = [..db.Rides
                    .Where(r => r.VehicleId == vehicleId)
                    .OrderBy(r => r.Start)
                    .Take(limit)];
                cache.Set((vehicleId, start, finish), uncachedRides);
                return uncachedRides;
            }
            
            uncachedRides = [..db.Rides
                .Where(r => r.VehicleId == vehicleId
                            && r.Start >= start 
                            && r.Finish <= finish)
                .OrderBy(r => r.Start).Take(limit)];
            cache.Set((vehicleId, start, finish), uncachedRides);
            return uncachedRides;
        }
    }
}
