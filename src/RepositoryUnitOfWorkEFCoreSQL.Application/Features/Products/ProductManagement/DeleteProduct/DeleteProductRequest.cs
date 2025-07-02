using Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.DeleteProduct;

public class DeleteProductRequest : IRequest
{
    public string Id { get; set; }
}