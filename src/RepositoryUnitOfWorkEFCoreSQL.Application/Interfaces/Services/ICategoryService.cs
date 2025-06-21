using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Categories.Requests;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Categories.Responses;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;

public interface ICategoryService
{
    Task<List<CategoryGridResponse>> GetListAsync(CancellationToken cancellationToken);
    Task CreateAsync(CategoryCreateRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
