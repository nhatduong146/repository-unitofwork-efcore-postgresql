using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;
using System.Linq.Expressions;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Repositories;

public interface IBaseRepository<T> where T : BaseEntity
{
    IQueryable<T> GetDbSet();
    Task<T?> GetAsync(string id, CancellationToken cancellationToken = default);
    Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<List<T>> GetListAsync(CancellationToken cancellationToken = default);
    Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<long> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(string id, CancellationToken cancellationToken = default);
    Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default);
    Task InsertAsync(T entity, CancellationToken cancellationToken = default);
    Task InsertRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task RemoveAsync(T entity, CancellationToken cancellationToken = default);
    Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
}
