using Gateway.Application.DataValidations;
using Gateway.Application.Map;
using Gateway.Application.Services;
using Gateway.Core.Interfaces.Repository;
using Gateway.Core.Responses;
using Gateway.Infrastructure;
using Gateway.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplicationModule(this IServiceCollection services)
    {
        services
            .GetInfrastructureModule()
            .AddRepositoryes()
            .AddScopes();
        return services;
    }

    public static IServiceCollection GetInfrastructureModule(this IServiceCollection services)
    {
        services.AddInfrastructureModule();
        return services;
    }

    public static IServiceCollection AddRepositoryes(this IServiceCollection service)
    {
        service.AddScoped<ITransactionRepository, TransactionRepository>();
        return service;
    }

    public static IServiceCollection AddScopes(this IServiceCollection service)
    {
        service.AddScoped<DataService>();
        service.AddScoped<InternalResponse>();
        service.AddScoped<TransactionMapper>();
        service.AddScoped<TransactionValidation>();
        return service;
    }
}
