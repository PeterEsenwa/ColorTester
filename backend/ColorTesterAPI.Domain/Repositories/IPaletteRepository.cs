using ColorTesterAPI.Domain.Entities;

namespace ColorTesterAPI.Domain.Repositories;

public interface IPaletteQueryRepository
{
    Task<IEnumerable<Palette>> GetPalettesAsync();
    Task<Palette?> GetPaletteAsync(Guid id);
}

public interface IPaletteCommandRepository
{
    Task AddPaletteAsync(Palette palette);
    Task UpdatePaletteAsync(Palette palette);
    Task DeletePaletteAsync(Guid id);
}