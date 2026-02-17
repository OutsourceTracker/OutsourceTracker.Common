using OutsourceTracker.Geolocation;  // Adjust namespace if needed for MapCoordinates
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Converters;  // Suggested namespace; place in a shared project for Blazor WASM and API

/// <summary>
/// Custom JSON converter for <see cref="MapCoordinates"/> to handle serialization/deserialization as a JSON object.
/// This converter ensures the struct serializes to {"latitude": double, "longitude": double, "accuracy": double}
/// with camelCase naming, matching your example request body. It can be used in both Blazor WASM frontend
/// and .NET Web API backend for consistent handling during PUT requests or other API interactions.
/// If using global JsonSerializerOptions with PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
/// this converter is optional for basic serialization but useful for custom logic (e.g., validation).
/// </summary>
public class NullableMapCoordinatesConverter : JsonConverter<MapCoordinates?>
{
    public override MapCoordinates? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token for MapCoordinates.");
        }

        double lat = 0, lng = 0, acc = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                // Optional: Add validation here (e.g., lat between -90 and 90)
                if (lat < -90 || lat > 90) throw new JsonException("Latitude must be between -90 and 90.");
                if (lng < -180 || lng > 180) throw new JsonException("Longitude must be between -180 and 180.");

                return new MapCoordinates(lat, lng, acc);
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString()?.ToLowerInvariant();
                reader.Read();  // Move to value

                switch (propertyName)
                {
                    case "latitude":
                        lat = reader.GetDouble();
                        break;
                    case "longitude":
                        lng = reader.GetDouble();
                        break;
                    case "accuracy":
                        acc = reader.GetDouble();
                        break;
                    default:
                        // Ignore unknown properties or throw
                        break;
                }
            }
        }

        throw new JsonException("Unexpected end when reading MapCoordinates.");
    }

    public override void Write(Utf8JsonWriter writer, MapCoordinates? value, JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            writer.WriteNullValue();
            return;
        }

        MapCoordinates vlu = value.Value;
        writer.WriteStartObject();

        // Use camelCase manually; alternatively, rely on options if set globally
        writer.WriteNumber("latitude", vlu.Latitude);
        writer.WriteNumber("longitude", vlu.Longitude);
        writer.WriteNumber("accuracy", vlu.Accuracy);

        writer.WriteEndObject();
    }
}