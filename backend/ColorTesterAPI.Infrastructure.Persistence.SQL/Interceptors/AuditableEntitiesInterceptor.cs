using ColorTesterAPI.Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace ColorTesterAPI.Infrastructure.Persistence.SQL.Interceptors;

public class AuditableEntitiesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData dbContextEventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = default)
    {
        var entries = dbContextEventData.Context?.ChangeTracker.Entries<AuditableEntity>();

        if (entries == null) return base.SavingChangesAsync(dbContextEventData, result, cancellationToken);
        
        foreach (var entry in entries)
        {
            if (entry.State is EntityState.Added or EntityState.Modified)
            {
                entry.Entity.MarkUpdated();
            }
        }

        return base.SavingChangesAsync(dbContextEventData, result, cancellationToken);
    }
}