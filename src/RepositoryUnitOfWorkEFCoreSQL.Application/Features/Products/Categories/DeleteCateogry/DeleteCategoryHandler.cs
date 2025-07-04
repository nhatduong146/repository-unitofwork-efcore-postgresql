﻿using Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.DeleteCateogry;

public class DeleteCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryRequest>
{
    public async ValueTask<Unit> Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Categories.GetAsync(request.Id, cancellationToken)
            ?? throw new Exception("Not found category");

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;

        await unitOfWork.ExecuteTransactionAsync<Task>(async () =>
        {
            await unitOfWork.Categories.UpdateAsync(category, cancellationToken);
            await unitOfWork.Products.BulkDeleteByCategoryAsync(request.Id, cancellationToken);
            return unitOfWork.SaveChangesAsync(cancellationToken);
        });

        return default;
    }
}
