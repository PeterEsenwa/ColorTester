using System.Text.Json.Serialization;
using ColorTesterAPI.Domain.Entities;
using ColorTesterAPI.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace ColorTesterAPI.Infrastructure.Persistence.SQL;

public class ColorTesterDbContext : DbContext
{
    public ColorTesterDbContext(DbContextOptions<ColorTesterDbContext> options)
        : base(options)
    {
    }

    // DbSet properties representing your tables in the PostgreSQL database
    public DbSet<Palette> Palettes { get; set; } = null!;

    // Fluent API configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Palette>(entity =>
        {
            entity.ToTable("palettes");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Colors)
                .HasConversion(
                    c => JsonConvert.SerializeObject(c),
                    c => JsonConvert.DeserializeObject<Dictionary<ColorRole, PaletteColor>>(c) ?? new Dictionary<ColorRole, PaletteColor>()
                ).HasColumnName("colors")
                .HasColumnType("jsonb")
                .Metadata.SetValueComparer(new ValueComparer<Dictionary<ColorRole, PaletteColor>>(
                    (c1, c2) => c1 != null && c2 != null && c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => c.ToDictionary(v => v.Key, v => v.Value)
                ));
        });
    }
}