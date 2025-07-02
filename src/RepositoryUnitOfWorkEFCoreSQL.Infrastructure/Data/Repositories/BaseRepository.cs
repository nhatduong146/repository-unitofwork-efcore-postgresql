using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;
using System.Linq.Expressions;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;

public class BaseRepository<T>(AppDbContext appDbContext) : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbSet<T> DbSet = appDbContext.Set<T>();

    protected IQueryable<T> GetDbSet() => DbSet;

    public virtual Task<T?> GetAsync(string id, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(_ => _.Id == id && !_.IsDeleted, cancellationToken);
    }

    public virtual Task<T?> GetAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.FirstOrDefaultAsync(expression, cancellationToken);
    }

    public virtual Task<List<T>> GetListAsync(CancellationToken cancellationToken = default)
    {
        return DbSet.Where(_ => !_.IsDeleted).ToListAsync(cancellationToken);
    }

    public virtual Task<List<T>> GetListAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.Where(expression).ToListAsync(cancellationToken);
    }

    public virtual Task<bool> AnyAsync(string id, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(_ => _.Id == id && !_.IsDeleted, cancellationToken);
    }

    public virtual Task<bool> AnyAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.AnyAsync(expression, cancellationToken);
    }

    public Task<long> CountAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken = default)
    {
        return DbSet.LongCountAsync(expression, cancellationToken);
    }

    public virtual Task InsertAsync(T entity, CancellationToken cancellationToken = default)
    {
        return DbSet.AddAsync(entity, cancellationToken).AsTask();
    }

    public virtual async Task InsertRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities?.Any() == true)
            await DbSet.AddRangeAsync(entities, cancellationToken);
    }

    public virtual Task RemoveAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual Task RemoveRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities?.Any() == true)
            DbSet.RemoveRange(entities);
        return Task.CompletedTask;
    }

    public virtual Task UpdateAsync(T entity, CancellationToken cancellationToken = default)
    {
        DbSet.Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        if (entities?.Any() == true)
            DbSet.UpdateRange(entities);
        return Task.CompletedTask;
    }
}
