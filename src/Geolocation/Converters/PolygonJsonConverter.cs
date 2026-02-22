using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OutsourceTracker.Geolocation.Converters;

public class PolygonJsonConverter : JsonConverter<Polygon>
{
    public override Polygon Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array for Polygon points.");
        }

        var points = new List<Vector2>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
            {
                break;
            }

            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException("Expected start of coordinate array [x,y].");
            }

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException("Expected number for X coordinate.");
            float x = reader.GetSingle();

            reader.Read();
            if (reader.TokenType != JsonTokenType.Number)
                throw new JsonException("Expected number for Y coordinate.");
            float y = reader.GetSingle();

            points.Add(new Vector2(x, y));

            reader.Read();
            if (reader.TokenType != JsonTokenType.EndArray)
                throw new JsonException("Expected end of coordinate array.");
        }

        return new Polygon(points);
    }

    public override void Write(Utf8JsonWriter writer, Polygon value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();

        var span = value.Points.Span;
        for (int i = 0; i < value.VertexCount; i++)
        {
            var p = span[i];
            writer.WriteStartArray();
            writer.WriteNumberValue(p.X);
            writer.WriteNumberValue(p.Y);
            writer.WriteEndArray();
        }

        writer.WriteEndArray();
    }
}
