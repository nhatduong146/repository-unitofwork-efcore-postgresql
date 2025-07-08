using Mapster;
using Microsoft.EntityFrameworkCore;
using RepositoryUnitOfWorkEFCoreSQL.Application.Common.Mediator;
using RepositoryUnitOfWorkEFCoreSQL.Application.Configurations;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces;
using RepositoryUnitOfWorkEFCoreSQL.Domain.Interfaces.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Contexts;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Data.Repositories;
using RepositoryUnitOfWorkEFCoreSQL.Infrastructure.Mediator;

namespace RepositoryUnitOfWorkEFCoreSQL.Api;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Register Mapster
        services.AddMapster();
        MapsterConfig.RegisterMappings();

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

        // Register Midiator
        services.AddMediator(typeof(IMediator).Assembly);

        return services;
    }
}
