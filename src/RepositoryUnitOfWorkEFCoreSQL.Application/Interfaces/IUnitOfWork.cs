using System.Data;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}