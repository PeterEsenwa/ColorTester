namespace ColorTesterAPI.Domain.ValueObjects;

public class ColorDictionary : PaletteColorValue
{
    public ColorDictionary(Dictionary<ColorIntensity, Color> value)
    {
        Value = value;
    }

    public Dictionary<ColorIntensity, Color> Value { get; private set; }
}