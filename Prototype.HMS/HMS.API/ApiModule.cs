using HMS.Application.UseCases;
using HMS.Core.Interfaces.Messaging;
using HMS.Infrastructure.Messaging;

namespace HMS.API
{
    public static class ApiModule
    {
        public static IServiceCollection AddModuleApi(this IServiceCollection services)
        {
            services.AddUseCases();
            return services;
        }

        public static IServiceCollection AddUseCases(this IServiceCollection service) 
        {
            service.AddScoped<ClientUseCases>();
            return service;
        } 

    }
}
