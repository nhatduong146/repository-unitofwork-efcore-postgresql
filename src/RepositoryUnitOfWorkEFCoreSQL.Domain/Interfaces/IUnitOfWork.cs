using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using System.Data;

namespace RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

public interface IUnitOfWork
{
    IProductRepostitory Products { get; }
    ICategoryRepository Categories { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    Task<TResult> ExecuteTransactionAsync<TResult>(Func<Task<TResult>> func, IsolationLevel isolationLevel = IsolationLevel.ReadCommitted);
}