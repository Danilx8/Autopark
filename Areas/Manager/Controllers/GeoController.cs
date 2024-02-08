using Autopark.Data;
using Autopark.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;
using NetTopologySuite.IO;
using Newtonsoft.Json;
using NuGet.Protocol;

namespace Autopark.Areas.Manager.Controllers
{
    [ApiController]
    public class GeoController(ApplicationDbContext db) : BaseManagerController(db)
    {
        [HttpGet]
        [Route("{vehicleId}")]
        public IActionResult GetPaths(
            int vehicleId, [FromQuery] DateTime start, [FromQuery] DateTime finish,
            [FromHeader] string timeZoneId, [FromHeader] bool displayGeoJson = false)
        {
            Vehicle vehicle = _db.Vehicles.Find(vehicleId)!;
            if (vehicle == null)
            {
                return BadRequest("Vehicle id is invalid");
            }

            if (!TimeZoneInfo.TryFindSystemTimeZoneById(timeZoneId, out TimeZoneInfo? timeZoneInfo)) 
                return BadRequest("Specified time zone can't be parsed");

            List<Geopoint> path = [.. _db
                .Points
                .Where(p => p.VehicleId == vehicle.Id 
                && p.RegisterTime >= start 
                && p.RegisterTime <= finish)];

            path.ForEach(p => p.RegisterTime = TimeZoneInfo.ConvertTimeFromUtc(p.RegisterTime, timeZoneInfo));

            if (!displayGeoJson) return Ok(path);

            var geoSerializer = GeoJsonSerializer.Create();
            using var geoStringWriter = new StringWriter();
            using var geoJsonWriter = new JsonTextWriter(geoStringWriter);
            geoSerializer.Serialize(geoJsonWriter, path);

            return Ok(geoStringWriter.ToString());
        }
    }
}
