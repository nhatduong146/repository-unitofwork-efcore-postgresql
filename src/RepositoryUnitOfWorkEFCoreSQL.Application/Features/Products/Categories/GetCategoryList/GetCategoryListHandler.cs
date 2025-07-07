using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.GetCategoryList;

public class GetCategoryListHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryListRequest, List<GetCategoryListResponse>>
{
    public async Task<List<GetCategoryListResponse>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Categories.GetListAsync(cancellationToken);
        return categories.Adapt<List<GetCategoryListResponse>>();
    }
}
