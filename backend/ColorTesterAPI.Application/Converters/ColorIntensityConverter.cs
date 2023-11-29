using ColorTesterAPI.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ColorTesterAPI.Application.Converters;

public class ColorIntensityConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ColorIntensity);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var token = JToken.Load(reader);
        switch (token.Type)
        {
            case JTokenType.Integer:
                return Enum.ToObject(typeof(ColorIntensity), token.ToObject<int>());

            case JTokenType.String when Enum.TryParse<ColorIntensity>(token.ToString(), out var result):
                return result;

            default:
                throw new JsonSerializationException("Unexpected token type for ColorIntensity: " + token.Type);
        }
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is ColorIntensity colorIntensity)
            writer.WriteValue(colorIntensity.ToString());
        else
            throw new JsonSerializationException("Unexpected type when converting ColorIntensity");
    }
}