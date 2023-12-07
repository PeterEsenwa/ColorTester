using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Domain.Interfaces;

internal interface IPaletteColor
{
    ColorRole Role { get; }
    PaletteColorValue Value { get; }
}