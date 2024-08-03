using HMS.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace HMS.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        private readonly string connectionTest = "Server=localhost,1433;" +
                                                        "Database=HmsPrototype;" +
                                                        "User Id=SA;" +
                                                        "Password=MyStrongPassword123!;" +
                                                        "TrustServerCertificate=True;";

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
           // Construtor sem Injecao de Dependencia
        public DataContext() {
            
        }
        // Para criar um context novo, assim resolvendo um erro que possui ao realizar os comandos: 
        // dotnet ef migrations add NomeDaMigracao
        // dotnet ef database update
        // ------------------------------------------
        // Este problema só ocorre ao iniciar o esta camada separadamente, pois ela precisa do appsettings


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(connectionTest);
            }
        }

        public DbSet<Client> Clients { get; set; }


        //Adicionar as regras da entidade
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Client>().ToTable("Clients");

        }
    }
}
