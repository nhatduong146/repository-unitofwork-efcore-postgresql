using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using Microsoft.AspNetCore.Mvc;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.DeleteCateogry;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.CreateCategory;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.GetCategoryList;

namespace RepositoryUnitOfWorkEFCoreSQL.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<List<GetCategoryListResponse>> GetCategories(CancellationToken cancellationToken)
    {
        return mediator.Send(new GetCategoryListRequest(), cancellationToken);
    }

    [HttpPost]
    public Task CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        return mediator.Send(request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public Task DeleteCategory(string id, CancellationToken cancellationToken)
    {
        var request = new DeleteCategoryRequest { Id = id };
        return mediator.Send(request, cancellationToken);
    }
}
