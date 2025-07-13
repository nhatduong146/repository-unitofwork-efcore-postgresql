using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

public class Review : BaseEntity
{
    public byte Rating { get; set; }
    public string? Comment { get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }

    public Guid CustomerId { get; set; }
    public User Customer { get; set; }
}
