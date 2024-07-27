using HMS.Infrastructure.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using HMS.Core.Interfaces.Repository;
using HMS.Infrastructure.Persistence.Repository;
using HMS.Infrastructure.Messaging;

namespace HMS.Infrastructure
{
    public static class InfrastructureModules
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services
                .AddContext(connectionString);
            return services;
        }
        /*public static IServiceCollection AddMessageBus(this IServiceCollection service)
        {
            service.AddHostedService<MessageBusService>();
            return service;
        }*/
        public static IServiceCollection AddContext(this IServiceCollection service, string connectionString)
        {
            service.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(connectionString); //CONTAINER COM A IMAGEM SQL
                //Server=localhost;Database=master;Trusted_Connection=True;[
                //Será substituido pelo container docker
            });
            return service;
        }
    }
}
