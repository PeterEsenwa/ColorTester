using ColorTesterAPI.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ColorTesterAPI.Infrastructure.Persistence.SQL;

public static class PersistenceSQL_DI
{
    public static IServiceCollection AddPostgresSQLDependencyInjection(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ColorTesterDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnection"));
        });

        return services;
    }
}