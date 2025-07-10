using Microsoft.Extensions.DependencyInjection;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;
using System.Data;
using System.Reflection;

namespace RepositoryUnitOfWorkEFCoreSQL.Mediator;

public static class DependencyInjection
{
    public static IServiceCollection AddMediator(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetExecutingAssembly();

        // Regsiter mediator
        services.AddScoped<IMediator, Mediator>();

        // Register request handlers
        services.AddRequestHandlers(assembly);

        return services;
    }

    private static void AddRequestHandlers(this IServiceCollection services, Assembly assembly)
    {
        Type[] handlerInterfaceTypes = [typeof(IRequestHandler<>), typeof(IRequestHandler<,>)];

        var handlerTypes = assembly.GetTypes()
            .Where(type => type.IsClass && !type.IsAbstract && type.GetInterfaces()
                .Any(i => i.IsGenericType && handlerInterfaceTypes.Contains(i.GetGenericTypeDefinition())));

        foreach (var handlerType in handlerTypes)
        {
            var interfaces = handlerType.GetInterfaces()
                .Where(i => i.IsGenericType && handlerInterfaceTypes.Contains(i.GetGenericTypeDefinition()));

            foreach (var handlerInterface in interfaces)
                services.AddScoped(handlerInterface, handlerType);
        }
    }
}
