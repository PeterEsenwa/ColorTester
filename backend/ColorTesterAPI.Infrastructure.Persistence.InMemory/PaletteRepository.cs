using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.Repositories;

namespace ColorTesterAPI.Infrastructure.Persistence.InMemory;

internal class PaletteRepository : IPaletteQueryRepository, IPaletteCommandRepository
{
    private readonly List<Palette> _palettes = new();

    public Task AddPaletteAsync(Palette palette)
    {
        _palettes.Add(palette);
        return Task.CompletedTask;
    }

    public Task UpdatePaletteAsync(Palette palette)
    {
        var existingPalette = _palettes.FirstOrDefault(p => p.Id == palette.Id);

        if (existingPalette == null) throw new ArgumentException("Palette not found");

        _palettes.Remove(existingPalette);
        _palettes.Add(palette);

        return Task.CompletedTask;
    }

    public Task DeletePaletteAsync(Guid id)
    {
        var existingPalette = _palettes.FirstOrDefault(p => p.Id == id);

        if (existingPalette == null) throw new ArgumentException("Palette not found");

        _palettes.Remove(existingPalette);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<Palette>> GetPalettesAsync()
    {
        return Task.FromResult(_palettes.AsEnumerable());
    }

    public Task<Palette?> GetPaletteAsync(Guid id)
    {
        return Task.FromResult(_palettes.FirstOrDefault(p => p.Id == id));
    }
}