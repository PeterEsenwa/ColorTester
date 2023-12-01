namespace ColorTesterAPI.Application.DTOs;

public class CreatePaletteRequest
{
    // constructor
    public List<PaletteColorDto> Colors { get; set; } = new();

    public List<PaletteColorDto> GetColors()
    {
        return Colors;
    }
}