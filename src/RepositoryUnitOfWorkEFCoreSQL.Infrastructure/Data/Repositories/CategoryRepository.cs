using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;

public class CategoryRepository(AppDbContext appDbContext) : BaseRepository<Category>(appDbContext), ICategoryRepository
{
}
