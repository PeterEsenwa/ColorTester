using ColorTesterAPI.Domain.Entities;

namespace ColorTesterAPI.Domain.Repositories;

public interface IPaletteQueryRepository
{
    Task<IEnumerable<Palette>> GetPalettesAsync(string userId);
    Task<Palette?> GetPaletteAsync(Guid id, string userId);
}

public interface IPaletteCommandRepository
{
    Task AddPaletteAsync(Palette palette);
    Task UpdatePaletteAsync(Palette palette);
    Task DeletePaletteAsync(Guid id);
}