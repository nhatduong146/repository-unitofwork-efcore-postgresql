using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.CreateCategory;

public class CreateCategoryRequest : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}
