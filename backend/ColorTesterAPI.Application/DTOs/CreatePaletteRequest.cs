using ColorTesterAPI.Domain.Entities;

namespace ColorTesterAPI.Application.DTOs;

public class CreatePaletteRequest
{
    public List<PaletteColorDto> Colors { get; set; } = new();
    
    public List<PaletteColorDto> GetColors()
    {
        return Colors;
    }
    
    // constructor
    public CreatePaletteRequest() { }
}