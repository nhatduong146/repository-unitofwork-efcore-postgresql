using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;
using System.Data;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data;

public class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return appDbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted)
    {
        if (appDbContext.Database.CurrentTransaction == null)
        {
            var strategy = appDbContext.Database.CreateExecutionStrategy();
            var transResult = await strategy.ExecuteAsync(async () =>
            {
                using var trans = await appDbContext.Database.BeginTransactionAsync(isolationLevel);
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
