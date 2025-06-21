using Microsoft.AspNetCore.Mvc;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Categories.Requests;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Categories.Responses;

namespace RepositoryUnitOfWorkEFCoreSQL.Api.Controllers;

[ApiController]
[Route("api/categories")]
public class CategoryController(ICategoryService categoryService) : ControllerBase
{
    [HttpGet]
    public Task<List<CategoryGridResponse>> GetCategories(CancellationToken cancellationToken)
    {
        return categoryService.GetListAsync(cancellationToken);
    }

    [HttpPost]
    public Task CreateCategory([FromBody] CategoryCreateRequest request, CancellationToken cancellationToken)
    {
        return categoryService.CreateAsync(request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public Task DeleteCategory(string id, CancellationToken cancellationToken)
    {
        return categoryService.DeleteAsync(id, cancellationToken);
    }
}
