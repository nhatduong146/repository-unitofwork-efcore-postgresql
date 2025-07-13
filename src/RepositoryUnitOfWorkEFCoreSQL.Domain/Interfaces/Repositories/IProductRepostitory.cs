using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;

public interface IProductRepostitory : IBaseRepository<Product>
{
    Task<string?> GetProductName(Guid productId, CancellationToken cancellationToken);
    Task<List<Product>> GetListAsync(Guid categoryId, CancellationToken cancellationToken);
    Task BulkDeleteByCategoryAsync(Guid categoryId, CancellationToken cancellationToken);
}