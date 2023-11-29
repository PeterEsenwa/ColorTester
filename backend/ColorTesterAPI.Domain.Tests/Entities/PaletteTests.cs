using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.ValueObjects;
using Xunit;

namespace ColorTesterAPI.Domain.Tests.Entities;

public class PaletteTests
{
    [Fact]
    public void PaletteBuilder_WithValidColors_CreatesPaletteSuccessfully()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .AddColor(new PaletteColor(ColorRole.Secondary, new Color(0, 0, 0)))
            .Build();

        Assert.Equal(2, palette.Colors.Count);
    }

    [Fact]
    public void PaletteBuilder_WithNoColors_ThrowsException()
    {
        var palette = Palette.Builder().Build();

        Assert.Empty(palette.Colors);
    }

    [Fact]
    public void PaletteBuilder_AddColor_AddsColorSuccessfully()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        palette.AddColor(new PaletteColor(ColorRole.Secondary, new Color(0, 0, 0)));

        Assert.Equal(2, palette.Colors.Count);
    }

    [Fact]
    public void PaletteBuilder_AddNullColor_ThrowsException()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        Assert.Throws<ArgumentNullException>(() => palette.AddColor(null!));
    }

    [Fact]
    public void PaletteBuilder_RemoveColor_RemovesColorSuccessfully()
    {
        var colorToRemove = new PaletteColor(ColorRole.Primary, new Color(255, 255, 255));
        var palette = Palette.Builder()
            .AddColor(colorToRemove)
            .Build();

        palette.RemoveColor(ColorRole.Primary);

        Assert.Empty(palette.Colors);
    }

    [Fact]
    public void PaletteBuilder_RemoveNonExistingColor_ReturnsFalse()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        Assert.False(palette.RemoveColor(ColorRole.Secondary));
    }
    
    [Fact]
    public void PaletteBuilder_AddColorWithExistingRole_ThrowsException()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        Assert.Throws<ArgumentException>(() => palette.AddColor(new PaletteColor(ColorRole.Primary, new Color(0, 0, 0))));
    }
    
    [Fact]
    public void PaletteBuilder_UpdateColor_UpdatesColorSuccessfully()
    {
        var colorToUpdate = new PaletteColor(ColorRole.Primary, new Color(255, 255, 255));
        var palette = Palette.Builder()
            .AddColor(colorToUpdate)
            .Build();

        palette.UpdateColor(new PaletteColor(ColorRole.Primary, new Color(0, 0, 0)));

        var colorByRole = palette.GetColorByRole(ColorRole.Primary);
        
        var colorValue = Assert.IsType<ColorValue>(colorByRole.Value);
        
        Assert.Equal(0, colorValue.Value.Red);
        Assert.Equal(0, colorValue.Value.Green);
        Assert.Equal(0, colorValue.Value.Blue);
    }
    
    [Fact]
    public void PaletteBuilder_GetColorByRole_ReturnsColorSuccessfully()
    {
        var colorToGet = new PaletteColor(ColorRole.Primary, new Color(255, 255, 255));
        var palette = Palette.Builder()
            .AddColor(colorToGet)
            .Build();

        var colorByRole = palette.GetColorByRole(ColorRole.Primary);
        
        Assert.Equal(colorToGet, colorByRole);
    }
    
    [Fact]
    public void PaletteBuilder_GetNonExistingColor_ThrowsException()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        Assert.Throws<ArgumentException>(() => palette.GetColorByRole(ColorRole.Secondary));
    }
    
    // IDs are generated on instantiation, so we can only test that they are not equal.
    [Fact]
    public void PaletteBuilder_Build_CreatesPaletteSuccessfully()
    {
        var palette = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        var palette2 = Palette.Builder()
            .AddColor(new PaletteColor(ColorRole.Primary, new Color(255, 255, 255)))
            .Build();

        Assert.NotEqual(palette.Id, palette2.Id);
    }
}