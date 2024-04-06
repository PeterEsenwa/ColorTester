using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ColorTesterAPI.Infrastructure.Persistence.SQL;

public class PaletteSqlRepository : IPaletteCommandRepository, IPaletteQueryRepository
{
    private readonly ColorTesterDbContext _context;

    public PaletteSqlRepository(ColorTesterDbContext context)
    {
        _context = context;
    }

    public async Task AddPaletteAsync(Palette palette)
    {
        await _context.Palettes.AddAsync(palette);
        await _context.SaveChangesAsync();
    }

    public async Task UpdatePaletteAsync(Palette palette)
    {
        _context.Palettes.Update(palette);
        await _context.SaveChangesAsync();
    }

    public async Task DeletePaletteAsync(Guid id)
    {
        var palette = await _context.Palettes.FindAsync(id);

        if (palette == null) return;

        _context.Palettes.Remove(palette);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Palette>> GetPalettesAsync(string userId)
    {
        return await _context.Palettes.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Palette?> GetPaletteAsync(Guid id, string userId)
    {
        return await _context.Palettes.FirstOrDefaultAsync(p => p.Id == id && p.UserId == userId);
    }
}