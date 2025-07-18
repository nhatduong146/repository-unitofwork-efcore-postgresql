﻿using Microsoft.AspNetCore.Mvc;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Resources;
using RepositoryUnitOfWorkEFCoreSQL.Application.Exceptions;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.CreateProduct;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.DeleteProduct;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.GetProductList;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.UpdateProduct;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Api.Controllers;

[ApiController]
[Route("api/products")]
public class ProductController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<List<GetProductListResponse>> GetAllProducts(CancellationToken cancellationToken)
    {
        return mediator.Send(new GetProductListRequest(), cancellationToken);
    }

    [HttpPost]
    public Task Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new BadRequestException(ErrorMessages.BadRequest);

        return mediator.Send(request, cancellationToken);
    }

    [HttpPut("{id}")]
    public Task UpdateProduct(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new BadRequestException(ErrorMessages.BadRequest);

        request.Id = id;
        return mediator.Send(request, cancellationToken);
    }

    [HttpDelete("{id}")]
    public Task DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var request = new DeleteProductRequest { Id = id };
        return mediator.Send(request, cancellationToken);
    }
}
