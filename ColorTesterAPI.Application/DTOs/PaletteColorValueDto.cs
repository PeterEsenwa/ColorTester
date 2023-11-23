using System.Text.Json;
using System.Text.Json.Serialization;
using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Application.DTOs;

public class PaletteColorValueDtoConverter : JsonConverter<PaletteColorValueDto>
{
    public override PaletteColorValueDto? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions? options)
    {
        using var doc = JsonDocument.ParseValue(ref reader);
        var root = doc.RootElement;
        
        var type = root.GetProperty("Type").GetInt32();
        switch (type)
        {
            case 0:
                return JsonSerializer.Deserialize<ColorValueDto>(root.GetRawText(), options);
            case 1:
                return JsonSerializer.Deserialize<ColorDictionaryDto>(root.GetRawText(), options);
            default:
                throw new InvalidOperationException("Unsupported PaletteColorValueDto type");
        }
    }

    public override void Write(Utf8JsonWriter writer, PaletteColorValueDto value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}

[JsonConverter(typeof(PaletteColorValueDtoConverter))]
public abstract class PaletteColorValueDto
{
    public ColorType Type { get; set; }
}

public class ColorDictionaryDto : PaletteColorValueDto
{
    public Dictionary<ColorIntensity, string> Value { get; set; }
}

public class ColorValueDto : PaletteColorValueDto
{
    public string Value { get; set; }
}