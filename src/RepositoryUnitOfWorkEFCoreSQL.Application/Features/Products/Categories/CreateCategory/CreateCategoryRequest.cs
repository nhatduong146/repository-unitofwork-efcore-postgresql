using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.CreateCategory;

public class CreateCategoryRequest : IRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
