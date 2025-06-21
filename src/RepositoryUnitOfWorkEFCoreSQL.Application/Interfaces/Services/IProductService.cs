using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Requests;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Responses;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;

public interface IProductService
{
    Task<List<ProductGridResponse>> GetAllAsync(CancellationToken cancellationToken);
    Task<ProductDetailResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
    Task CreateAsync(ProductCreateRequest request, CancellationToken cancellationToken);
    Task UpdateAsync(string id, ProductCreateRequest request, CancellationToken cancellationToken);
    Task DeleteAsync(string id, CancellationToken cancellationToken);
}
