using ColorTesterAPI.Domain.Abstract;
using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Domain.Entities;

public class Palette : AuditableEntity
{
    public Dictionary<ColorRole, PaletteColor> Colors = new();

    private Palette()
    {
    }

    public Guid Id { get; } = Guid.NewGuid();
    
    public string? Name { get; set; }
    
    public string UserId { get; set; } = null!;

    public void AddColor(PaletteColor color)
    {
        if (color == null)
            throw new ArgumentNullException(nameof(color));
        
        if (!Colors.TryAdd(color.Role, color))
            throw new ArgumentException($"The palette already contains a color with the role {color.Role}.");
    }

    public bool RemoveColor(ColorRole role)
    {
        return Colors.Remove(role);
    }

    public void UpdateColor(PaletteColor newColor)
    {
        Colors[newColor.Role] = newColor;
    }

    public PaletteColor GetColorByRole(ColorRole role)
    {
        if (!Colors.TryGetValue(role, out var color))
            throw new ArgumentException($"The palette does not contain a color with the role {role}.");
        return color;
    }

    public static PaletteBuilder Builder()
    {
        return new PaletteBuilder();
    }

    public class PaletteBuilder
    {
        private readonly Dictionary<ColorRole, PaletteColor> _colors = new();

        public PaletteBuilder AddColor(PaletteColor color)
        {
            _colors[color.Role] = color;
            return this;
        }

        public Palette Build()
        {
            return new Palette
            {
                Colors = new Dictionary<ColorRole, PaletteColor>(_colors)
            };
        }
    }
}