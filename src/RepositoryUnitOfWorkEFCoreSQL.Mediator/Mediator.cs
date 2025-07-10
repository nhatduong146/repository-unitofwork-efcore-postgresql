using Microsoft.Extensions.DependencyInjection;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;

namespace RepositoryUnitOfWorkEFCoreSQL.Mediator;

public class Mediator(IServiceProvider serviceProvider) : IMediator
{
    public Task Send(IRequest request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<>).MakeGenericType(request.GetType());

        dynamic handler = serviceProvider.GetRequiredService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for request type {request.GetType().FullName}");

        // Get pipeline behaviors for this request type
        var behaviorType = typeof(IPipelineBehavior<>).MakeGenericType(request.GetType());
        var behaviors = serviceProvider.GetServices(behaviorType).Cast<object>();

        // Build handler delegate
        RequestHandlerDelegate handlerDelegate = () => handler.Handle((dynamic)request, cancellationToken);

        // Apply behaviors in reverse order (so first registered runs first)
        foreach (var behavior in behaviors.Reverse())
        {
            var currentPipeline = handlerDelegate;
            handlerDelegate = () => ((dynamic)behavior).Handle((dynamic)request, currentPipeline, cancellationToken);
        }

        return handlerDelegate();
    }

    public Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
    {
        var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));

        dynamic handler = serviceProvider.GetRequiredService(handlerType)
            ?? throw new InvalidOperationException($"No handler registered for request type {request.GetType().FullName}");

        // Get pipeline behaviors for this request type
        var behaviorType = typeof(IPipelineBehavior<,>).MakeGenericType(request.GetType(), typeof(TResponse));
        var behaviors = serviceProvider.GetServices(behaviorType).Cast<object>();

        // Build handler delegate
        RequestHandlerDelegate<TResponse> handlerDelegate = () => handler.Handle((dynamic)request, cancellationToken);

        // Apply behaviors in reverse order (so first registered runs first)
        foreach (var behavior in behaviors.Reverse())
        {
            var currentPipeline = handlerDelegate;
            handlerDelegate = () => ((dynamic)behavior).Handle(request, currentPipeline, cancellationToken);
        }

        return handlerDelegate();
    }
}
