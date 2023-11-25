using ColorTesterAPI.Domain.ValueObjects;

namespace ColorTesterAPI.Application.DTOs;

public class PaletteColorDto
{
    public ColorRole Role { get; set; }
    public PaletteColorValueDto Value { get; set; }
}