using HMS.Application.Mapper;
using HMS.Application.UseCases;
using HMS.Core.Interfaces.Messaging;
using HMS.Core.Interfaces.Repository;
using HMS.Infrastructure.Messaging;
using HMS.Infrastructure.Persistence.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace HMS.Application
{
    public static class ApplicationModule 
    {
        public static IServiceCollection AddModuleApplication(this IServiceCollection services)
        {
            services
                .AddMapper()
                .AddRepository()
                .AddMessagery();
            return services;
        }

        public static IServiceCollection AddMapper(this IServiceCollection service)
        {
            service.AddScoped<ClientMapper>();
            return service;
        }
        public static IServiceCollection AddRepository(this IServiceCollection service)
        {
            service.AddScoped<IClientRepository, ClientRepository>();
            return service;
        }
        public static IServiceCollection AddMessagery(this IServiceCollection service)
        {
            // Há, talvez, um pouco de gambiarra no MessageBusProcessor, devido ao MessageBusService
            // ser Singleton (da classe que ele herda). Se possivel, refazer aquilo futuramente!


            service.AddScoped<IMessageBusService, MessageBusService>();
            service.AddSingleton<IMessageBusProcessor, MessageBusProcessor>();
            service.AddHostedService<MessageBusService>();
            return service;
        }
 
    }
}
