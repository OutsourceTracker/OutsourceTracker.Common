using System.Text.Json;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Geolocation.Converters;

public class NullableVector2JsonConverter : JsonConverter<Vector2?>
{
    public override Vector2? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject)
        {
            throw new JsonException("Expected StartObject token for MapCoordinates.");
        }

        double lat = 0, lng = 0;

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndObject)
            {
                return new Vector2(lat, lng);
            }

            if (reader.TokenType == JsonTokenType.PropertyName)
            {
                var propertyName = reader.GetString()?.ToLowerInvariant();
                reader.Read();

                switch (propertyName)
                {
                    case "x":
                        lat = reader.GetDouble();
                        break;
                    case "y":
                        lng = reader.GetDouble();
                        break;
                    default:
                        break;
                }
            }
        }

        throw new JsonException("Unexpected end when reading Vector2.");
    }

    public override void Write(Utf8JsonWriter writer, Vector2? value, JsonSerializerOptions options)
    {
        if (!value.HasValue)
        {
            writer.WriteNullValue();
            return;
        }

        Vector2 val = value.Value;
        writer.WriteStartObject();
        writer.WriteNumber("x", val.X);
        writer.WriteNumber("y", val.Y);
        writer.WriteEndObject();
    }
}
