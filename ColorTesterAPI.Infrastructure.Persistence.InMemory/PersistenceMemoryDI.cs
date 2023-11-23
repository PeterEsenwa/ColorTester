using ColorTesterAPI.Application;
using ColorTesterAPI.Domain.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace ColorTesterAPI.Infrastructure.Persistence.InMemory;

public static class PersistenceMemoryDI
{
    public static IServiceCollection AddInMemoryPersistence(this IServiceCollection services)
    {
        services.AddScoped<IPaletteQueryRepository, PaletteRepository>();
        services.AddScoped<IPaletteCommandRepository, PaletteRepository>();
        services.AddScoped<PaletteApplicationService>();
        return services;
    }
}