using ColorTesterAPI.Domain.Abstract;
using Xunit;

namespace ColorTesterAPI.Domain.Tests.Abstract;

public class AuditableEntityTests
{
    private class TestEntity : AuditableEntity { }

    [Fact]
    public void MarkUpdated_SetsUpdatedAtToCurrentTime()
    {
        var entity = new TestEntity();

        entity.MarkUpdated();
        
        Assert.NotNull(entity.UpdatedAt);

        Assert.True((DateTime.UtcNow - entity.UpdatedAt.Value).TotalSeconds < 1);
    }

    [Fact]
    public void MarkDeleted_SetsDeletedAtToCurrentTime()
    {
        var entity = new TestEntity();

        entity.MarkDeleted();
        
        Assert.NotNull(entity.DeletedAt);

        Assert.True((DateTime.UtcNow - entity.DeletedAt.Value).TotalSeconds < 1);
    }

    [Fact]
    public void MarkCreated_SetsCreatedAtToCurrentTime()
    {
        var entity = new TestEntity();
        
        Thread.Sleep(1000);

        Assert.True(entity.CreatedAt.AddSeconds(1) < DateTime.UtcNow);
    }
}