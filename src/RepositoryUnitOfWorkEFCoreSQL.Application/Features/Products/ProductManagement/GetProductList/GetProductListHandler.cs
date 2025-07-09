using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductManagement.GetProductList;

public class GetProductListHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetProductListRequest, List<GetProductListResponse>>
{
    public async Task<List<GetProductListResponse>> Handle(GetProductListRequest request, CancellationToken cancellationToken)
    {
        var products = await unitOfWork.Products.GetListAsync(cancellationToken);
        return products.Adapt<List<GetProductListResponse>>();
    }
}
