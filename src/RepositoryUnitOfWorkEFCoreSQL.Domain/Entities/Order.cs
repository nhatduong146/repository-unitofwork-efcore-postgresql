using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

public class Order : BaseEntity
{
    public string OrderCode { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public string Status { get; set; }

    public ICollection<OrderItem> OrderItems { get; set; }
}
