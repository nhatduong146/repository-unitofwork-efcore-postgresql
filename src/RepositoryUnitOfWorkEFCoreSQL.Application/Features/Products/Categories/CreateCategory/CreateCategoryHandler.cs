using Mapster;
using Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.CreateCategory;

public class CreateCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateCategoryRequest>
{
    public async ValueTask<Unit> Handle(CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = request.Adapt<Category>();

        await unitOfWork.Categories.InsertAsync(category, cancellationToken);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return default;
    }
}
