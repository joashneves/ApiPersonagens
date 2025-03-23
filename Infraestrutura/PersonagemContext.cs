using System.Diagnostics;
using ApiBotDiscord.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiBotDiscord.Infraestrutura
{
    public class PersonagemContext : DbContext
    {
        private IConfiguration _configuration;
        public DbSet<Personagem> PersonagemSet { get; set; }

        public PersonagemContext(IConfiguration configuration, DbContextOptions<PersonagemContext> options)
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
