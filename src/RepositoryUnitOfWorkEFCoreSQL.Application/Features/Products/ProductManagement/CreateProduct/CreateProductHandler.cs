using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.CreateProduct;

public class CreateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductRequest>
{
    public async Task Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = request.Adapt<Product>();

        await unitOfWork.Products.InsertAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
