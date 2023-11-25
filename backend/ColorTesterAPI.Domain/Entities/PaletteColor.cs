using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Domain.Entities;

public class PaletteColor
{
    public Guid Id { get; private set; }
    public ColorRole Role { get; private set; }
    public PaletteColorValue Value { get; private set; }
    
    public PaletteColor(ColorRole role, PaletteColorValue value)
    {
        Id = Guid.NewGuid();
        Role = role;
        Value = value;
    }
    
    public PaletteColor(ColorRole role, Dictionary<ColorIntensity, Color> value)
        : this(role, new ColorDictionary(value)) { }
    
    public PaletteColor(ColorRole role, Color value)
        : this(role, new ColorValue(value)) { }

    public PaletteColor(ColorRole role, string value)
        : this(role, new Color(value)) { }
}