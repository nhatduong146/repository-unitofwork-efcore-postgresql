using Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.DeleteProduct;

public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductRequest>
{
    public async ValueTask<Unit> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetAsync(request.Id, cancellationToken);
        if (product == null) return default;

        await unitOfWork.Products.RemoveAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return default;
    }
}
