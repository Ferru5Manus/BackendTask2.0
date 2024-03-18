using AbonentsDbService.Models;
using Microsoft.EntityFrameworkCore;

namespace AbonentsDbService.Database;

public class ApplicationContext : DbContext
{
    public DbSet<Abonent> Abonents { get; set; }
    public DbSet<Passport> Passports { get; set; }
    public DbSet<Contract> Contracts { get; set; }

    public ApplicationContext()
    {
        Database.EnsureCreated();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(Environment.GetEnvironmentVariable("CONNECTION_STRING"));
    }
}