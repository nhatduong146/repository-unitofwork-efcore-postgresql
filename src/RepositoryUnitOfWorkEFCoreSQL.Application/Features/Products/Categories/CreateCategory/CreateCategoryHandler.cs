using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.CreateCategory;

public class CreateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryRequest>
{
    public async Task Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = request.Adapt<Category>();

        await unitOfWork.Categories.InsertAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
