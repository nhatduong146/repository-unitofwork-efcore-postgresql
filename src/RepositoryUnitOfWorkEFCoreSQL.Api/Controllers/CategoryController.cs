using Mediator;
using Microsoft.AspNetCore.Mvc;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.CreateCategory;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.DeleteCateogry;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.GetCategoryList;

namespace RepositoryUnitOfWorkEFCoreSQL.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public ValueTask<List<GetCategoryListResponse>> GetCategories(CancellationToken cancellationToken)
    {
        return mediator.Send(new GetCategoryListRequest(), cancellationToken);
    }

    [HttpPost]
    public ValueTask<Unit> CreateCategory([FromBody] CreateCategoryRequest request, CancellationToken cancellationToken)
    {
        return mediator.Send(request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public ValueTask<Unit> DeleteCategory(string id, CancellationToken cancellationToken)
    {
        var request = new DeleteCategoryRequest { Id = id };
        return mediator.Send(request, cancellationToken);
    }
}
