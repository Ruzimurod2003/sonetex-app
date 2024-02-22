using Microsoft.EntityFrameworkCore;
using SonetexApp.Models;

namespace SonetexApp.Data;

public class ApplicationContext : DbContext
{
    public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
    {

    }
    public DbSet<Culture> Cultures { get; set; }
    public DbSet<Models.File> Files { get; set; }
    public DbSet<Resource> Resources { get; set; }
    public DbSet<Configuration> Configurations { get; set; }
    public DbSet<Catalog> Catalogs { get; set; }
    public DbSet<Models.Type> Types { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Specialist> Specialists { get; set; }
    public DbSet<State> States { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Culture>().HasData(
            new List<Culture>()
            {
                new Culture() { Id = 1, Name = "en" },
                new Culture() { Id = 2, Name = "ru" },
                new Culture() { Id = 3, Name = "uz" }
            });

        modelBuilder.Entity<State>().HasData(
            new List<State>()
            {
                new State() { Id = 1, Name = "Янги", NameUzbek = "Yangi", NameRussian = "Новый", NameEnglish = "New" },
                new State() { Id = 2, Name = "Сақлаш жойидан", NameUzbek = "Saqlash joyidan", NameRussian = "С хранения", NameEnglish = "From storage" }
            });
    }
}