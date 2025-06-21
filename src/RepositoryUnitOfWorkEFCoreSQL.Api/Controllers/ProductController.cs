using Microsoft.AspNetCore.Mvc;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Requests;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Responses;

namespace RepositoryUnitOfWorkEFCoreSQL.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IProductService productService) : ControllerBase
{
    [HttpGet]
    public Task<List<ProductGridResponse>> GetAllProducts(CancellationToken cancellationToken)
    {
        return productService.GetAllAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    public Task<ProductDetailResponse> GetProductById(string id, CancellationToken cancellationToken)
    {
        return productService.GetByIdAsync(id, cancellationToken);
    }

    [HttpPost]
    public Task Create([FromBody] ProductCreateRequest request, CancellationToken cancellationToken)
    {
        return productService.CreateAsync(request, cancellationToken);
    }

    [HttpPut("{id}")]
    public Task UpdateProduct(string id, [FromBody] ProductCreateRequest request, CancellationToken cancellationToken)
    {
        return productService.UpdateAsync(id, request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public Task DeleteProduct(string id, CancellationToken cancellationToken)
    {
        return productService.DeleteAsync(id, cancellationToken);
    }
}
