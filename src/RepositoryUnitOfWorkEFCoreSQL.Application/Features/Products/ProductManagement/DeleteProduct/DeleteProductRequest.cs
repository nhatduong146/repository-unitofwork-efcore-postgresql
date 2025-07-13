using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.DeleteProduct;

public class DeleteProductRequest : IRequest
{
    public Guid Id { get; set; }
}