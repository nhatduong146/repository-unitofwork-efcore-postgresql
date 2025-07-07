using Microsoft.Extensions.DependencyInjection;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Mediator;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());

        dynamic handler = serviceProvider.GetRequiredService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for request type {request.GetType().FullName}");

        return handler.Handle((dynamic)request, cancellationToken);
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = serviceProvider.GetRequiredService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for request type {request.GetType().FullName}");

        return handler.Handle((dynamic)request, cancellationToken);

    }
}
