using System.Globalization;
using Newtonsoft.Json;

namespace ColorTesterAPI.Domain.ValueObjects;

public class Color
{
    [JsonConstructor]
    public Color(int red, int green, int blue, int? alpha = null)
    {
        if (!IsValueValid(red) || !IsValueValid(green) || !IsValueValid(blue))
            throw new ArgumentOutOfRangeException(nameof(alpha), "All color parameters must be between 0 and 255.");

        if (alpha is not null && !IsValueValid(alpha.Value))
            throw new ArgumentOutOfRangeException(nameof(alpha), "Alpha parameter must be between 0 and 255.");

        Alpha = alpha;
        Red = red;
        Green = green;
        Blue = blue;
    }

    public Color(string? hex)
    {
        if (string.IsNullOrEmpty(hex))
            throw new ArgumentException("Hexadecimal color cannot be null or empty.");
        
        (Alpha, Red, Green, Blue) = HexColorParser.Parse(hex);
    }

    private static class HexColorParser
    {
        public static (int? alpha, int red, int green, int blue) Parse(string hex)
        {
            CheckHexLength(hex);
            CheckHexPrefix(hex);

            int? alpha = null;
            if (hex.Length is 9)
            {
                ParseColorComponent(hex, 1, 3, out var alphaValue);
                alpha = alphaValue;
            }

            ParseColorComponent(hex, hex.Length - 6, hex.Length - 4, out var red);
            ParseColorComponent(hex, hex.Length - 4, hex.Length - 2, out var green);
            ParseColorComponent(hex, hex.Length - 2, hex.Length, out var blue);

            return (alpha, red, green, blue);
        }
    }

    public int Red { get; }
    public int Green { get; }
    public int Blue { get; }
    public int? Alpha { get; }

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