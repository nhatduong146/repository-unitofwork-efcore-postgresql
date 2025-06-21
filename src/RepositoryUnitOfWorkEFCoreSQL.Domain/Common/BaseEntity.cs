using System.ComponentModel.DataAnnotations;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

public class BaseEntity
{
    public BaseEntity()
    {
        Id = Guid.NewGuid().ToString();
        CreatedAt = DateTime.UtcNow;
    }

    [Key]
    public string Id { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
    public bool IsDeleted { get; set; }
}