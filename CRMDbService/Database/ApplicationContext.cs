using CRMDbService.Models;
using Microsoft.EntityFrameworkCore;

namespace CRMDbService.Database;

public class ApplicationContext : DbContext
{
   public DbSet<Application> Applications { get; set; }
   public DbSet<Service> Services { get; set; }
   public DbSet<TechnicalSupportCall> TechnicalSupportCalls { get; set; }

   public ApplicationContext()
   {
      Database.EnsureCreated();
   }

   protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
   {
      optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
   }
}