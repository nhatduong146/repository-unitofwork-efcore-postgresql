using Mapster;
using Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.CreateProduct;

public class CreateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductRequest>
{
    public async ValueTask<Unit> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        var product = request.Adapt<Product>();

        await unitOfWork.Products.InsertAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return default;
    }
}
