using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

public class ProductImage : BaseEntity
{
    public string ImageUrl { get; set; }
    public string? AltText { get; set; }
    public bool IsThumbnail { get; set; }
    public double Index {  get; set; }

    public Guid ProductId { get; set; }
    public Product Product { get; set; }
}
