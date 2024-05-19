using System.ComponentModel.DataAnnotations;
using ColorTesterAPI.Domain.Abstract;

namespace ColorTesterAPI.Domain.Entities;

public class Project : AuditableEntity
{
    [Key]
    public Guid Id { get; } = Guid.NewGuid();

    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public string? Description { get; set; }
    public Guid? PaletteId { get; set; }
    public string MockupName { get; set; } = null!;
}