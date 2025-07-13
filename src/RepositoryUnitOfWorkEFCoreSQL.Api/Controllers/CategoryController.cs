using Microsoft.AspNetCore.Mvc;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Resources;
using RepositoryUnitOfWorkEFCoreSQL.Application.Exceptions;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.CreateCategory;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.GetCategoryList;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.DeleteCateogry;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

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
        if (request == null)
            throw new BadRequestException(ErrorMessages.BadRequest);

        return mediator.Send(request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public Task DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteCategoryRequest { Id = id };
        return mediator.Send(request, cancellationToken);
    }
}
