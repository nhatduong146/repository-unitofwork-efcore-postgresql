using Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.CreateCategory;

public class CreateCategoryRequest : IRequest
{
    public string Name { get; set; }
    public string Description { get; set; }
}
