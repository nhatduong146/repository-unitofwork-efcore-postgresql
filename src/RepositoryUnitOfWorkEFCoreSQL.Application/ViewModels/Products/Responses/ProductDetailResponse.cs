namespace RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Responses;

public class ProductDetailResponse
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }

    public string CategoryId { get; set; }
    public string CategoryName { get; set; }

}
