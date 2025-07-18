﻿using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Resources;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.DeleteCateogry;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.DeleteCateogry;

public class DeleteCategoryHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCategoryRequest>
{
    public async Task Handle(DeleteCategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await unitOfWork.Categories.GetAsync(request.Id, cancellationToken)
            ?? throw new Exception(ErrorMessages.CategoryNotFound);

        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;

        await unitOfWork.ExecuteTransactionAsync<Task>(async () =>
        {
            await unitOfWork.Categories.UpdateAsync(category, cancellationToken);
            await unitOfWork.Products.BulkDeleteByCategoryAsync(request.Id, cancellationToken);
            return unitOfWork.SaveChangesAsync(cancellationToken);
        });
    }
}
