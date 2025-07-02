using Mapster;
using Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.UpdateProduct;

public class UpdateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductRequest>
{
    public async ValueTask<Unit> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetAsync(request.Id, cancellationToken)
            ?? throw new Exception("Product not found");

        request.Adapt(product);

        await unitOfWork.Products.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return default;
    }
}
