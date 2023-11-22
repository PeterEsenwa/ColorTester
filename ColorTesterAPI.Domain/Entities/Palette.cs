using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Domain.Entities
{
    public class Palette
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        private Dictionary<ColorRole, PaletteColor> _colors = new();

        private Palette() { }

        public void AddColor(PaletteColor color)
        {
            if (!_colors.TryAdd(color.Role, color))
                throw new ArgumentException($"The palette already contains a color with the role {color.Role}.");
        }

        public bool RemoveColor(ColorRole role)
        {
            return _colors.Remove(role);
        }

        public void UpdateColor(PaletteColor newColor)
        {
            _colors[newColor.Role] = newColor;
        }

        public List<PaletteColor> GetColors()
        {
            return _colors.Values.ToList();
        }

        public PaletteColor GetColorByRole(ColorRole role)
        {
            if (!_colors.TryGetValue(role, out var color))
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
                    _colors = new Dictionary<ColorRole, PaletteColor>(_colors)
                };
            }
        }
    }
}