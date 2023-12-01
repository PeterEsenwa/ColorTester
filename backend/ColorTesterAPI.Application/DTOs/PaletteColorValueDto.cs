using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Application.DTOs;

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