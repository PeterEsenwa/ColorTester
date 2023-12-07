using ColorTesterAPI.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ColorTesterAPI.Application.Converters;

// Can be used to convert any inherited class of PaletteColorValue
// to and from JSON. Like ColorValue and ColorDictionary.
public class PaletteColorValueConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(PaletteColorValue);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        switch (value)
        {
            case ColorValue colorValue:
                serializer.Serialize(writer, colorValue);
                break;
            case ColorDictionary colorDictionary:
                serializer.Serialize(writer, colorDictionary);
                break;
            case null:
                writer.WriteNull();
                break;
            default:
                throw new InvalidOperationException("Unsupported PaletteColorValue type");
        }
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        // Case-insensitive search for "type" property
        var jToken = jsonObject["type"] ?? jsonObject["Type"];

        if (jToken == null)
            throw new JsonSerializationException("Missing 'type' property in PaletteColorValue");

        // jToken can be string or integer representation of ColorType

        var colorType = jToken.Type switch
        {
            JTokenType.Integer => Enum.ToObject(typeof(ColorType), jToken.ToObject<int>()),
            JTokenType.String => Enum.Parse(typeof(ColorType), jToken.ToString(), true),
            _ => throw new JsonSerializationException("Unexpected token type for ColorType: " + jToken.Type)
        };
        
        // accept type as string or integer
        return colorType switch
        {
            ColorType.Color => jsonObject.ToObject<ColorValue>(serializer) ??
                               throw new InvalidOperationException("ColorValue is null"),
            ColorType.Dictionary => jsonObject.ToObject<ColorDictionary>(serializer) ??
                                    throw new InvalidOperationException("ColorDictionary is null"),
            _ => throw new InvalidOperationException("Unsupported PaletteColorValue type")
        };
    }
}