using Mapster;
using Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.ProductCategories.GetCategoryList;

public class GetCategoryListHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryListRequest, List<GetCategoryListResponse>>
{
    public async ValueTask<List<GetCategoryListResponse>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Categories.GetListAsync(cancellationToken);
        return categories.Adapt<List<GetCategoryListResponse>>();
    }
}
