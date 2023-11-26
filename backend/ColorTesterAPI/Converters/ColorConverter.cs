using ColorTesterAPI.Domain.ValueObjects;
using Newtonsoft.Json;

namespace ColorTesterAPI.Converters;

public class ColorConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(Color);
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        if (reader.TokenType != JsonToken.String)
            throw new JsonSerializationException(
                $"Unexpected token parsing color. Expected String, got {reader.TokenType}.");

        return reader.Value == null ? null : new Color((string)reader.Value);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var color = (Color)value;
        writer.WriteValue(color.ToString());
    }
}