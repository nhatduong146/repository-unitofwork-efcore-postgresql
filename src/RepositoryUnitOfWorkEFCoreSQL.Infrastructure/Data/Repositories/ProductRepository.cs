using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;

public class ProductRepository(AppDbContext appDbContext) : BaseRepository<Product>(appDbContext), IProductRepostitory
{
    public Task<string?> GetProductName(string productId, CancellationToken cancellationToken)
    {
        return GetDbSet().AsNoTracking()
            .Where(_ => _.Id == productId)
            .Select(_ => _.Name)
            .FirstOrDefaultAsync(cancellationToken);
    }

    public Task<List<Product>> GetListAsync(string categoryId, CancellationToken cancellationToken)
    {
        return GetDbSet().AsNoTracking()
            .Where(_ => _.CategoryId == categoryId)
            .Include(_ => _.Category)
            .ToListAsync(cancellationToken);
    }

    public Task BulkDeleteByCategoryAsync(string categoryId, CancellationToken cancellationToken)
    {
        return GetDbSet()
            .Where(_ => _.CategoryId == categoryId)
            .ExecuteUpdateAsync(_ => _
                .SetProperty(p => p.IsDeleted, true)
                .SetProperty(p => p.DeletedBy, "system")
                .SetProperty(p => p.DeletedAt, DateTime.UtcNow), cancellationToken);
    }
}
