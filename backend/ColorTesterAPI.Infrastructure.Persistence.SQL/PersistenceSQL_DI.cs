using ColorTesterAPI.Application;
using ColorTesterAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ColorTesterAPI.Infrastructure.Persistence.SQL;

public static class PersistenceSQL_DI
{
    public static IServiceCollection AddPostgresSQLDependencyInjection(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ColorTesterDbContext>(options =>
        {
            options.UseNpgsql("Host=localhost;Port=5432;Database=color_tester;Username=postgres;Password=postgres");
        });

        services.AddScoped<IPaletteQueryRepository, PaletteSqlRepository>();
        services.AddScoped<IPaletteCommandRepository, PaletteSqlRepository>();
        services.AddScoped<PaletteApplicationService>();

        return services;
    }
}