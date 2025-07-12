using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Resources;
using RepositoryUnitOfWorkEFCoreSQL.Application.Exceptions;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.UpdateProduct;

public class UpdateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductRequest>
{
    public async Task Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        var product = await unitOfWork.Products.GetAsync(request.Id, cancellationToken)
            ?? throw new BadRequestException(ErrorMessages.ProductNotFound);

        request.Adapt(product);

        await unitOfWork.Products.UpdateAsync(product, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
