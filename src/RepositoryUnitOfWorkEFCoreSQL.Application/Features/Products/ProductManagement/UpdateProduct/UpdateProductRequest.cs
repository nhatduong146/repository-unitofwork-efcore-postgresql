using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.UpdateProduct;

public class UpdateProductRequest : IRequest
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public string CategoryId { get; set; }
}
