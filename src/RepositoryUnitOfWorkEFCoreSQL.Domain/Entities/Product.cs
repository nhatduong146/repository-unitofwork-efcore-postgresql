using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public bool IsActive { get; set; }

    public double AverageRating { get; set; }
    public int TotalReviews { get; set; }

    public Guid CategoryId { get; set; }
    public Category Category { get; set; }

    public Guid ShopId { get; set; }  
    public Shop Shop { get; set; } 

    public ICollection<Review> Reviews { get; set; }
    public ICollection<ProductImage> Images { get; set; }
    public ICollection<OrderItem> OrderItems { get; set; }
}
