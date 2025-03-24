using ApiBotDiscord.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBotDiscord.Infraestrutura
{
    public class ContaContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Conta> ContaSet { get; set; }

        public ContaContext(IConfiguration configuration, DbContextOptions<ContaContext> options)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            /*
            System.Console.WriteLine("DEBUG");
            var connectionString = _configuration.GetConnectionString("SQLData");
            System.Console.WriteLine("connectionString: " + connectionString);
            optionsBuilder.UseNpgsql(connectionString);
           */ 
            string? connectionString = Environment.GetEnvironmentVariable("SQLData");
            System.Console.WriteLine("connectionString: " + connectionString);
            optionsBuilder.UseNpgsql(connectionString);
            
        }
    }
}
