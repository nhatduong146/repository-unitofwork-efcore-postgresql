using Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.DeleteCateogry;

public class DeleteCategoryRequest : IRequest
{
    public string Id { get; set; }
}
