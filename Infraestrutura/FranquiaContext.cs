using ApiBotDiscord.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ApiBotDiscord.Infraestrutura
{
    public class FranquiaContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Franquia> FranquiaSet { get; set; }

        public FranquiaContext(IConfiguration configuration, DbContextOptions<FranquiaContext> options)
            : base(options)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            #if DEBUG
            System.Console.WriteLine("DEBUG");
            var connectionString = _configuration.GetConnectionString("SQLData");
            System.Console.WriteLine("connectionString: " + connectionString);
            optionsBuilder.UseNpgsql(connectionString);
            #else
            string? connectionString = Environment.GetEnvironmentVariable("SQLData");
            System.Console.WriteLine("connectionString: " + connectionString);
            optionsBuilder.UseNpgsql(connectionString);
            #endif
        }
    }
}
