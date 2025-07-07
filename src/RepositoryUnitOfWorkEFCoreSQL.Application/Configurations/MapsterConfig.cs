using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.GetProductList;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Configurations;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        #region Products
        TypeAdapterConfig<Product, GetProductListResponse>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
        #endregion
    }
}
