using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Application.Interfaces.Services;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Requests;
using RepositoryUnitOfWorkEFCoreSQL.Application.ViewModels.Products.Responses;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Services;

public class ProductService(IUnitOfWork unitOfWork,
    IProductRepostitory productRepository) : BaseService<Product>(unitOfWork), IProductService
{
    public async Task<List<ProductGridResponse>> GetAllAsync(CancellationToken cancellationToken)
    {
        var products = await productRepository.GetListAsync(cancellationToken);
        return products.Adapt<List<ProductGridResponse>>();
    }

    public async Task<ProductDetailResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(id, cancellationToken)
            ?? throw new Exception("Product not found");

        return product.Adapt<ProductDetailResponse>();
    }

    public async Task CreateAsync(ProductCreateRequest request, CancellationToken cancellationToken)
    {
        var product = request.Adapt<Product>();

        await productRepository.InsertAsync(product, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(string id, ProductCreateRequest request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(id, cancellationToken)
            ?? throw new Exception("Product not found");

        request.Adapt(product);

        await productRepository.UpdateAsync(product, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(string id, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetAsync(id, cancellationToken);
        if (product == null) return;

        await productRepository.RemoveAsync(product, cancellationToken);
        await UnitOfWork.SaveChangesAsync(cancellationToken);
    }
}
