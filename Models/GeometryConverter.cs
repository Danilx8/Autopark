using NetTopologySuite.Geometries;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Autopark.Models
{
    public class GeometryConverter : JsonConverter<Point>
    {
        public override Point? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, Point value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("X Coordinate");
            writer.WriteNumberValue(value.X);
            writer.WritePropertyName("Y Coordinate");
            writer.WriteNumberValue(value.Y);
            writer.WriteEndObject();
        }
    }
}
