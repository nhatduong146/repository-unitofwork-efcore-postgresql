using Mapster;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Application.Features.Products.Categories.GetCategoryList;

public class GetCategoryListHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetCategoryListRequest, List<GetCategoryListResponse>>
{
    public async Task<List<GetCategoryListResponse>> Handle(GetCategoryListRequest request, CancellationToken cancellationToken)
    {
        var categories = await unitOfWork.Categories.GetListAsync(cancellationToken);
        return categories.Adapt<List<GetCategoryListResponse>>();
    }
}
