namespace RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

public interface IPipelineBehavior<TRequest> where TRequest : IRequest
{
    Task Handle(TRequest request, RequestHandlerDelegate next, CancellationToken cancellationToken);
}

public interface IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken);
}


public delegate Task RequestHandlerDelegate();

public delegate Task<TResponse> RequestHandlerDelegate<TResponse>();