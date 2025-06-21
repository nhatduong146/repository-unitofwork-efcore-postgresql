namespace RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Requests;

public class ProductUpdateRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryId { get; set; }
}
