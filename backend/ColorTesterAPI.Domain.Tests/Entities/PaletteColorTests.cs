using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.ValueObjects;
using Xunit;

namespace ColorTesterAPI.Domain.Tests.Entities;

public class PaletteColorTests
{
    [Fact]
    public void PaletteColor_ConstructorWithColorRoleAndColorValue_CreatesPaletteColorSuccessfully()
    {
        var paletteColor = new PaletteColor(ColorRole.Primary, new ColorValue(new Color(255, 255, 255)));

        Assert.Equal(ColorRole.Primary, paletteColor.Role);
        Assert.IsType<ColorValue>(paletteColor.Value);
    }

    [Fact]
    public void PaletteColor_ConstructorWithColorRoleAndColorDictionary_CreatesPaletteColorSuccessfully()
    {
        var colorDictionary = new Dictionary<ColorIntensity, Color>
        {
            { ColorIntensity.Intensity0, new Color(255, 255, 255) },
            { ColorIntensity.Intensity900, new Color(0, 0, 0) }
        };

        var paletteColor = new PaletteColor(ColorRole.Primary, colorDictionary);

        Assert.Equal(ColorRole.Primary, paletteColor.Role);
        Assert.IsType<ColorDictionary>(paletteColor.Value);
    }

    [Fact]
    public void PaletteColor_ConstructorWithColorRoleAndColor_CreatesPaletteColorSuccessfully()
    {
        var paletteColor = new PaletteColor(ColorRole.Primary, new Color(255, 255, 255));

        Assert.Equal(ColorRole.Primary, paletteColor.Role);
        Assert.IsType<ColorValue>(paletteColor.Value);
    }

    [Fact]
    public void PaletteColor_ConstructorWithColorRoleAndString_CreatesPaletteColorSuccessfully()
    {
        var paletteColor = new PaletteColor(ColorRole.Primary, "#FFFFFF");

        Assert.Equal(ColorRole.Primary, paletteColor.Role);
        Assert.IsType<ColorValue>(paletteColor.Value);
    }

    [Fact]
    public void PaletteColor_ConstructorWithInvalidString_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new PaletteColor(ColorRole.Primary, "invalid"));
    }
    
    [Fact]
    public void PaletteColor_ConstructorWithNullColorValue_ThrowsException()
    {
        Assert.Throws<ArgumentNullException>(() => new PaletteColor(ColorRole.Primary, (PaletteColorValue)null!));
    }
    
    // compare generated GUIDs of two palette colors not equal
    [Fact]
    public void PaletteColor_GeneratesDifferentIds()
    {
        var paletteColor1 = new PaletteColor(ColorRole.Primary, new ColorValue(new Color(255, 255, 255)));
        var paletteColor2 = new PaletteColor(ColorRole.Primary, new ColorValue(new Color(255, 255, 255)));

        Assert.NotEqual(paletteColor1.Id, paletteColor2.Id);
    }
}