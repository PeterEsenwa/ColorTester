using ColorTesterAPI.Domain.Entities;

namespace ColorTesterAPI.Application.DTOs;

public class CreatePaletteRequest
{
    public List<PaletteColor> Colors { get; set; }
}