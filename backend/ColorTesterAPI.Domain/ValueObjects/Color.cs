using System.Globalization;

namespace ColorTesterAPI.Domain.ValueObjects;

public class Color
{
    public int Red { get; private set; }
    public int Green { get; private set; }
    public int Blue { get; private set; }
    public int? Alpha { get; private set; }

    private Color() {}
        
    public Color(int red, int green, int blue, int? alpha)
    {
        if (!IsValueValid(red) || !IsValueValid(green) || !IsValueValid(blue))
        {
            throw new ArgumentOutOfRangeException(nameof(alpha), "All color parameters must be between 0 and 255.");
        }
        
        if (alpha is not null && !IsValueValid(alpha.Value))
        {
            throw new ArgumentOutOfRangeException(nameof(alpha), "Alpha parameter must be between 0 and 255.");
        }
        
        Alpha = alpha;
        Red = red;
        Green = green;
        Blue = blue;
    }

    public Color(string hex)
    {
        CheckHexLength(hex);
        CheckHexPrefix(hex);

        if (hex.Length is 9)
        {
            ParseColorComponent(hex, 1, 3, out var alpha);
            Alpha = alpha;
        }
        
        ParseColorComponent(hex, hex.Length - 6, hex.Length - 4, out var red);
        ParseColorComponent(hex, hex.Length - 4, hex.Length - 2, out var green);
        ParseColorComponent(hex, hex.Length - 2, hex.Length, out var blue);

        Red = red;
        Green = green;
        Blue = blue;
    }

    private static void CheckHexLength(string hex)
    {
        if (hex.Length is 7 or 9) return;
        throw new ArgumentException("Hexadecimal color must be in the format #RRGGBB or #AARRGGBB.");
    }

    private static void CheckHexPrefix(string hex)
    {
        if (hex[0] is '#') return;
        throw new ArgumentException("Hexadecimal color must start with #.");
    }

    private static void ParseColorComponent(string hex, int start, int end, out int colorComponent)
    {
        if (int.TryParse(hex[start..end], NumberStyles.HexNumber, null, out colorComponent)) return;
        throw new ArgumentException("Hexadecimal color must be in the format #RRGGBB or #AARRGGBB.");
    }

    public override string ToString()
    {
        return Alpha is null
            ? $"#{Red:X2}{Green:X2}{Blue:X2}"
            : $"#{Alpha:X2}{Red:X2}{Green:X2}{Blue:X2}";
    }

    private static bool IsValueValid(int value)
    {
        return value is >= 0 and <= 255;
    }
}