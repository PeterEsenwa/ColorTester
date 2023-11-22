using ColorTesterAPI.Application.DTOs;
using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.Repositories;

namespace ColorTesterAPI.Application;

public class PaletteApplicationService
{
    private readonly IPaletteCommandRepository _paletteCommandRepository;
    private readonly IPaletteQueryRepository _paletteQueryRepository;

    public PaletteApplicationService(
        IPaletteCommandRepository paletteCommandRepository,
        IPaletteQueryRepository paletteQueryRepository)
    {
        _paletteCommandRepository = paletteCommandRepository;
        _paletteQueryRepository = paletteQueryRepository;
    }

    public async Task<Guid> CreatePaletteAsync(CreatePaletteRequest request)
    {
        var paletteBuilder = Palette.Builder();
        foreach (var color in request.Colors)
        {
            paletteBuilder.AddColor(new PaletteColor(color.Role, color.Value));
        }

        var newPalette = paletteBuilder.Build();
        await _paletteCommandRepository.AddPaletteAsync(newPalette);

        return newPalette.Id;
    }

    public async Task<IEnumerable<Palette>> GetPalettesAsync()
    {
        return await _paletteQueryRepository.GetPalettesAsync();
    }

    // Other methods for Get, Update, Delete
}