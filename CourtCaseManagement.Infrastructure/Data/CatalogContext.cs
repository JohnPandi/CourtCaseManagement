using CourtCaseManagement.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace CourtCaseManagement.Infrastructure.Data
{
    public class CatalogContext : DbContext
    {
        public CatalogContext(DbContextOptions<CatalogContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            if (!string.IsNullOrEmpty(connectionString))
            {
                try
                {
                    optionsBuilder.UseNpgsql(connectionString);
                    
                    Console.WriteLine($"| Add ConnectionString : {connectionString}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("| Classe: CatalogContext ");
                    Console.WriteLine("| Metodo: OnConfiguring ");
                    Console.WriteLine($"| Exception - Message: {ex.Message}");
                    Console.WriteLine($"| Exception - StackTrace: {ex.StackTrace}");
                }
            }
        }

        public DbSet<ProcessEntity> Company { get; set; }
        public DbSet<SituationEntity> Situation { get; set; }
        public DbSet<ResponsibleEntity> Responsible { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            try
            {
                base.OnModelCreating(builder);
                builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            }
            catch (Exception ex)
            {
                Console.WriteLine("| Classe: CatalogContext ");
                Console.WriteLine("| Metodo: OnModelCreating ");
                Console.WriteLine($"| Exception - Message: {ex.Message}");
                Console.WriteLine($"| Exception - StackTrace: {ex.StackTrace}");
            }
        }
    }
}