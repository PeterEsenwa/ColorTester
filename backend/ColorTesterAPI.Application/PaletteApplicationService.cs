using ColorTesterAPI.Application.DTOs;
using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.Repositories;
using ColorTesterAPI.Domain.ValueObjects;

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
            // Convert PaletteColorValueDto to PaletteColorValue
            PaletteColorValue colorValue;
            switch (color.Value)
            {
                case ColorDictionaryDto colorDictionaryDto:
                    colorValue = new ColorDictionary(colorDictionaryDto.Value.ToDictionary(
                        entry => entry.Key,
                        entry => new Color(entry.Value)));
                    break;
                case ColorValueDto colorValueDto:
                    colorValue = new ColorValue(new Color(colorValueDto.Value));
                    break;
                default:
                    throw new InvalidOperationException("Unsupported PaletteColorValueDto type");
            }
            paletteBuilder.AddColor(new PaletteColor(color.Role, colorValue));
        }

        var newPalette = paletteBuilder.Build();
        await _paletteCommandRepository.AddPaletteAsync(newPalette);

        return newPalette.Id;
    }

    public async Task<IEnumerable<Palette>> GetPalettesAsync()
    {
        return await _paletteQueryRepository.GetPalettesAsync();
    }
    
    public async Task<Palette?> GetPaletteAsync(Guid id)
    {
        return await _paletteQueryRepository.GetPaletteAsync(id);
    }

    public async Task UpdatePaletteAsync(Guid id, CreatePaletteRequest request)
    {
        // var palette = await _paletteQueryRepository.GetPaletteAsync(id);
        // if (palette == null)
        // {
        //     throw new ArgumentException($"Palette with id {id} not found");
        // }
        //
        // var paletteBuilder = Palette.Builder();
        // foreach (var color in request.GetColors())
        // {
        //     paletteBuilder.AddColor(new PaletteColor(color.Role, color.Value));
        // }
        //
        // var newPalette = paletteBuilder.Build();
        // await _paletteCommandRepository.UpdatePaletteAsync(newPalette);
    }
}