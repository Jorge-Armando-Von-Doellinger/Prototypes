using Gateway.Application;
using Gateway.Application.UseCases;

namespace Gateway.API;

public static class ApiModule
{
    public static IServiceCollection AddApiModule(this IServiceCollection services)
    {
        services
            .GetApplicationModule()
            .AddScopes();
        return services;
    }

    public static IServiceCollection GetApplicationModule(this IServiceCollection services)
    {
        services.AddApplicationModule();
        return services;
    }

    public static IServiceCollection AddScopes(this IServiceCollection service)
    {
        service.AddScoped<ProcessTransactions>();
        return service;
    }
}
