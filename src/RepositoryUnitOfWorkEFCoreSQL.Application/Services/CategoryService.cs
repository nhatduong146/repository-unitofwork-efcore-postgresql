using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Categories.Requests;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Categories.Responses;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Services;

public class CategoryService(IUnitOfWork unitOfWork,
    ICategoryRepository categoryRepository,
    IProductRepostitory productRepostitory) : BaseService<Category>(unitOfWork), ICategoryService
{
    public async Task<List<CategoryGridResponse>> GetListAsync(CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetListAsync(cancellationToken);
        return categories.Adapt<List<CategoryGridResponse>>();
    }

    public async Task CreateAsync(CategoryCreateRequest request, CancellationToken cancellationToken)
    {
        var category = request.Adapt<Category>();
        await categoryRepository.InsertAsync(category, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var category = await categoryRepository.GetAsync(id, cancellationToken)
            ?? throw new Exception("Not found category");
        
        category.IsDeleted = true;
        category.DeletedAt = DateTime.UtcNow;

        await UnitOfWork.ExecuteTransactionAsync<Task>(async () =>
        {
            await categoryRepository.UpdateAsync(category, cancellationToken);
            await productRepostitory.BulkDeleteByCategoryAsync(id, cancellationToken);
            return UnitOfWork.SaveChangesAsync(cancellationToken);
        });
    }
}