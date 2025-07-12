using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Behaviors;
using RepositoryUnitOfWorkEFCoreSQL.Application.Configurations;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Mediator.Intefaces;
using System.Reflection;

namespace RepositoryUnitOfWorkEFCoreSQL.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register Mapster
        services.AddMapster();
        MapsterConfig.RegisterMappings();

        // Register Mediator
        services.AddMediator(Assembly.Load("RepositoryUnitOfWorkEFCoreSQL.Application"));

        // Register validation pipeline behaviors
        services.AddScoped(typeof(IPipelineBehavior<>), typeof(ValidationBehavior<>));
        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        // Register Fluent validators
        services.AddValidatorsFromAssembly(Assembly.Load("RepositoryUnitOfWorkEFCoreSQL.Application"));

        // Disable automatic model state validation due to using Fluent validation instead
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }

    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        // Register AppDbContext
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
        });

        // Register UnitOfWork
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        // Register Generic Repositories
        services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

        return services;
    }
}
