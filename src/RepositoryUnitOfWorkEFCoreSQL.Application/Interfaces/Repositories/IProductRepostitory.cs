using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Repositories;

public interface IProductRepostitory : IBaseRepository<Product>
{
    Task<string?> GetProductName(string productId, CancellationToken cancellationToken);
    Task<List<Product>> GetListAsync(string categoryId, CancellationToken cancellationToken);
    Task BulkDeleteByCategoryAsync(string categoryId, CancellationToken cancellationToken);
}