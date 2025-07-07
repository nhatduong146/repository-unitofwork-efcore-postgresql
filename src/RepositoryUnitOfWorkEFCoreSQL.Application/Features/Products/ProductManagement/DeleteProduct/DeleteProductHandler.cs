using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.DeleteProduct;

public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductRequest>
{
    public async Task Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetAsync(request.Id, cancellationToken);
        if (product == null) return;

        await unitOfWork.Products.RemoveAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
