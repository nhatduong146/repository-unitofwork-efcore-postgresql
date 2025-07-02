using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;
using System.Data;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private IProductRepostitory? _productRepository;
    private ICategoryRepository? _categoryRepository;

    public IProductRepostitory Products => _productRepository ??= new ProductRepository(context);
    public ICategoryRepository Categories => _categoryRepository ??= new CategoryRepository(context);

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return context.SaveChangesAsync(cancellationToken);
    }

    public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (context.Database.CurrentTransaction == null)
        {
            var strategy = context.Database.CreateExecutionStrategy();
            var transResult = await strategy.ExecuteAsync(async () =>
            {
                using var trans = await context.Database.BeginTransactionAsync(isolationLevel);
                try
                {
                    var result = await func.Invoke();
                    await trans.CommitAsync();
                    return result;
                }
                catch (Exception)
                {
                    await trans.RollbackAsync();
                    throw;
                }
            });

            return transResult;
        }
        else
        {
            return await func.Invoke();
        }
    }
}
