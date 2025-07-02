using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Models.Products.Responses;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Entities;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Configurations;

public static class MapsterConfig
{
    public static void RegisterMappings()
    {
        #region Products
        TypeAdapterConfig<Product, ProductGridResponse>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category.Name);

        TypeAdapterConfig<Product, ProductDetailResponse>.NewConfig()
            .Map(dest => dest.CategoryName, src => src.Category.Name);
        #endregion
    }
}
