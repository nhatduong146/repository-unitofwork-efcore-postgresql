namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.GetProductList;

public class GetProductListResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public string CategoryId { get; set; }
    public string CategoryName { get; set; }
}
