namespace ColorTesterAPI.Domain.ValueObjects;

public class ColorValue : PaletteColorValue
{
    public ColorValue(Color value)
    {
        Value = value;
    }

    public Color Value { get; private set; }
}