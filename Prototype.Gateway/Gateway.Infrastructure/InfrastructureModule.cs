using System.Data;
using Gateway.Infrastructure.Collection;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddCollections();
        return services;
    }

    public static IServiceCollection AddCollections(this IServiceCollection service)
    {
        service.AddScoped<Collections>();
        return service;
    }
}
