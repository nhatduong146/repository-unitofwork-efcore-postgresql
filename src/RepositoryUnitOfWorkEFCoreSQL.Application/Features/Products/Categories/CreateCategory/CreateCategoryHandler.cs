using FluentValidation;
using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

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
