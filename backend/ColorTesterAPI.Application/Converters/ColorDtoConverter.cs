using ColorTesterAPI.Application.DTOs;
using ColorTesterAPI.Domain.ValueObjects;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

namespace ColorTesterAPI.Application.Converters;

public class ColorDtoConverter : JsonConverter
{
    private const string PropertyName = "type";

    private static ColorType GetPaletteColorType(JObject jsonObject)
    {
        var jToken = jsonObject[PropertyName];

        if (jToken == null)
            throw new JsonSerializationException($"Missing required property '{PropertyName}'.");

        try
        {
            return JsonConvert.DeserializeObject<ColorType>($"\"{jToken}\"");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new JsonSerializationException($"Unable to parse '{PropertyName}' value '{jToken}' to ColorType.");
        }
    }

    private static object ParseJsonObject(JToken jsonObject, JsonSerializer serializer, ColorType type)
    {
        return type switch
        {
            ColorType.Color => jsonObject.ToObject<ColorValueDto>(serializer) ??
                               throw new InvalidOperationException("ColorValueDto is null"),
            ColorType.Dictionary => jsonObject.ToObject<ColorDictionaryDto>(serializer) ??
                                    throw new InvalidOperationException("ColorDictionaryDto is null"),
            _ => throw new InvalidOperationException("Unsupported PaletteColorValueDto type")
        };
    }

    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(PaletteColorValueDto);
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue,
        JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var paletteColorType = GetPaletteColorType(jsonObject);

        // accept type as string or integer
        return ParseJsonObject(jsonObject, serializer, paletteColorType);
    }

    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        switch (value)
        {
            case ColorValueDto colorValueDto:
                serializer.Serialize(writer, colorValueDto);
                break;
            case ColorDictionaryDto colorDictionaryDto:
                serializer.Serialize(writer, colorDictionaryDto);
                break;
            case null:
                writer.WriteNull();
                break;
            default:
                throw new InvalidOperationException("Unsupported PaletteColorValueDto type");
        }
    }
}
