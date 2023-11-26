namespace ColorTesterAPI.Domain.Abstract;

public abstract class AuditableEntity
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }

    public void MarkUpdated()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public void MarkDeleted()
    {
        DeletedAt = DateTime.UtcNow;
    }
}
