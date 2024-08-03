
using Gateway.Infrastructure.Collection;
using Gateway.Infrastructure.Messaging;
using Gateway.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Infrastructure;

public static class InfrastructureModule
{
    public static IServiceCollection AddInfrastructureModule(this IServiceCollection services)
    {
        services
            .AddCollections()
            .AddChannelAndRelationals()
            .AddScopes();
        return services;
    }

    public static IServiceCollection AddCollections(this IServiceCollection service)
    {
        service.AddScoped<Collections>();
        return service;
    }

    public static IServiceCollection AddChannelAndRelationals(this IServiceCollection services)
    {
        services.AddSingleton<ChannelFactory>();
        return services;
    }

    public static IServiceCollection AddScopes(this IServiceCollection service)
    {
        service.AddScoped<JsonManipulationService>();
        return service;
    }
}
