using ColorTesterAPI.Application.DTOs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ColorTesterAPI.Application.Converters;

public class ColorDtoConverter : JsonConverter
{
    public override bool CanConvert(Type objectType)
    {
        return objectType == typeof(ColorValueDto) || objectType == typeof(ColorDictionaryDto) || objectType == typeof(PaletteColorValueDto);
    }
    
    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var jToken = jsonObject["type"];
        
        if (jToken == null)
            throw new JsonSerializationException("Missing 'type' property in PaletteColorValueDto");
        
        var type = jToken.Value<int>();
        
        var nestedSerializer = new JsonSerializer();
        
        foreach (var converter in serializer.Converters)
        {
            if (converter.GetType() != typeof(ColorDtoConverter))
            {
                nestedSerializer.Converters.Add(converter);
            }
        }
        
        switch (type)
        {
            case 0:
                return jsonObject.ToObject<ColorValueDto>(nestedSerializer) ?? throw new InvalidOperationException("ColorValueDto is null");
            case 1:
                return jsonObject.ToObject<ColorDictionaryDto>(nestedSerializer) ?? throw new InvalidOperationException("ColorDictionaryDto is null");
            default:
                throw new InvalidOperationException("Unsupported PaletteColorValueDto type");
        }
    }
    
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value == null)
        {
            writer.WriteNull();
            return;
        }

        var color = (PaletteColorValueDto)value;
        switch (color.Type)
        {
            case ColorType.Color:
                serializer.Serialize(writer, (ColorValueDto)color);
                break;
            case ColorType.Dictionary:
                serializer.Serialize(writer, (ColorDictionaryDto)color);
                break;
            default:
                throw new InvalidOperationException("Unsupported PaletteColorValueDto type");
        }
    }
}
