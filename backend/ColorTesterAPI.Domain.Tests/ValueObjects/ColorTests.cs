using ColorTesterAPI.Domain.ValueObjects;
using Xunit;

namespace ColorTesterAPI.Domain.Tests.ValueObjects;

public class ColorTests
{
    [Fact]
    public void CreateColor_WithValidRgbValues_CreatesColorSuccessfully()
    {
        var color = new Color(255, 255, 255, 255);

        Assert.Equal(255, color.Red);
        Assert.Equal(255, color.Green);
        Assert.Equal(255, color.Blue);
        Assert.Equal(255, color.Alpha);
    }

    [Fact]
    public void CreateColor_WithInvalidRgbValues_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(256, 255, 255));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(255, 256, 255));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(255, 255, 256));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(255, 255, 255, 256));
    }

    [Fact]
    public void CreateColor_WithNegativeRgbValues_ThrowsException()
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(-1, 255, 255));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(255, -1, 255));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(255, 255, -1));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Color(255, 255, 255, -1));
    }

    [Fact]
    public void Color_ToString_ReturnsCorrectFormat()
    {
        var color = new Color(255, 255, 255);

        Assert.Equal("#FFFFFF", color.ToString());
    }
    
    [Fact]
    public void CreateColor_WithValidHex_CreatesColorSuccessfully()
    {
        var color = new Color("#FFFFFF");

        Assert.Equal(255, color.Red);
        Assert.Equal(255, color.Green);
        Assert.Equal(255, color.Blue);
        Assert.Null(color.Alpha);
    }
    
    [Fact]
    public void CreateColor_WithValidHexAndAlpha_CreatesColorSuccessfully()
    {
        var color = new Color("#FFFFFFFF");

        Assert.Equal(255, color.Red);
        Assert.Equal(255, color.Green);
        Assert.Equal(255, color.Blue);
        Assert.Equal(255, color.Alpha);
    }
    
    [Fact]
    public void CreateColor_WithInvalidHex_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Color("#GGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("#GGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("#GGGGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("#GGGGGGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("#GGGGGGGGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("#GGGGGGGGGGGGGGGG"));
    }
    
    [Fact]
    public void CreateColor_WithInvalidHexPrefix_ThrowsException()
    {
        Assert.Throws<ArgumentException>(() => new Color("GGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("GGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("GGGGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("GGGGGGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("GGGGGGGGGGGGGG"));
        Assert.Throws<ArgumentException>(() => new Color("GGGGGGGGGGGGGGGG"));
    }
}