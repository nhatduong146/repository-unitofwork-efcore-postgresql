using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Common;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Services;

public abstract class BaseService<T>(IUnitOfWork unitOfWork) where T : BaseEntity
{
    protected IUnitOfWork UnitOfWork { get; } = unitOfWork;
}
