
using HMS.Application;
using HMS.Infrastructure;

namespace HMS.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddScoped<IServiceCollection, ServiceCollection>();

            builder.Services
                .AddModuleApi()
                .AddModuleApplication()
                .AddInfrastructure(builder.Configuration.GetConnectionString("DefaultConnection"));
            

            var app = builder.Build();
            
            // Configure the HTTP request pipeline.
            // if (app.Environment.IsDevelopment())
            
            app.UseSwagger();
            app.UseSwaggerUI();
            

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
