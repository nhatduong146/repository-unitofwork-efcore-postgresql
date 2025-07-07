using Microsoft.Extensions.DependencyInjection;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using System.Reflection;

namespace RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Mediator;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();

        // Register request handlers
        Type[] handlerInterfaceTypes = [typeof(IRequestHandler<>), typeof(IRequestHandler<,>)];

        var handlerTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract);

        foreach (var handlerType in handlerTypes)
        {
            var intefaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && handlerInterfaceTypes.Contains(i.GetGenericTypeDefinition()));

            foreach (var handlerInterface in intefaces)
                services.AddScoped(handlerInterface, handlerType);
        }

        // Regsiter mediator
        services.AddScoped<IMediator, Mediator>();

        return services;
    }
}
