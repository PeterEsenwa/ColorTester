using ColorTesterAPI.Application.Converters;
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

    private static Dictionary<ColorRole, PaletteColor> DoDeserializeObject(string json)
    {
        var settings = new JsonSerializerSettings();

        settings.Converters.Add(new PaletteColorValueConverter());

        var paletteColors = JsonConvert.DeserializeObject<Dictionary<ColorRole, PaletteColor>>(json, settings);
        return paletteColors ??
               new Dictionary<ColorRole, PaletteColor>();
    }

    // Fluent API configuration
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Palette>(entity =>
        {
            entity.ToTable("palettes");
            entity.HasKey(e => e.Id);
            entity.HasIndex(e => e.UserId);
            entity.HasIndex(e => e.Name).IsUnique().HasFilter("[name] IS NOT NULL");
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt).HasColumnName("created_at");
            entity.Property(e => e.UpdatedAt).HasColumnName("updated_at");
            entity.Property(e => e.DeletedAt).HasColumnName("deleted_at");
            entity.Property(e => e.Colors)
                .HasConversion(
                    c => JsonConvert.SerializeObject(c),
                    c => DoDeserializeObject(c)
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