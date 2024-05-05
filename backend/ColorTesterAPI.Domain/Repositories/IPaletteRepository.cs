using ColorTesterAPI.Domain.Entities;

namespace ColorTesterAPI.Domain.Repositories;

public interface IPaletteQueryRepository
{
    Task<IEnumerable<Palette>> GetPalettesAsync(string userId, int page = 1, int pageSize = 5);
    Task<Palette?> GetPaletteAsync(Guid id, string userId);
}

public interface IPaletteCommandRepository
{
    Task AddPaletteAsync(Palette palette);
    Task UpdatePaletteAsync(Palette palette);
    Task DeletePaletteAsync(Guid id);
}