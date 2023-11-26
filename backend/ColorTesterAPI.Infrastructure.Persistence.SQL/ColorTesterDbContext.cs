using ColorTesterAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;

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
            entity.Property(e => e.GetColors()).HasColumnName("colors");
        });
    }
}