using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.UpdateProduct;

public class UpdateProductRequest : IRequest
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public Guid CategoryId { get; set; }
}
