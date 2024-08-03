using Gateway.Application.DataValidations;
using Gateway.Application.Map;
using Gateway.Application.Services;
using Gateway.Core.Interfaces.Messaging;
using Gateway.Core.Interfaces.Repository;
using Gateway.Core.Responses;
using Gateway.Infrastructure;
using Gateway.Infrastructure.Messaging;
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
            .AddServices()
            .AddMessagePublisher()
            .AddMapers()
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

    public static IServiceCollection AddMessagePublisher(this IServiceCollection service)
    {
        service.AddScoped<IMessagePublisher, MessagePublisher>();
        return service;
    }

    public static IServiceCollection AddMapers(this IServiceCollection services)
    {
        services.AddScoped<Mapper>();
        services.AddScoped<MessageMapper>();
        services.AddScoped<TransactionMapper>();
        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<DataService>();
        services.AddScoped<MessageService>();
        return services;
    }

    public static IServiceCollection AddScopes(this IServiceCollection services)
    {
        services.AddScoped<InternalResponse>();
        services.AddScoped<TransactionValidation>();
        return services;
    }
}
