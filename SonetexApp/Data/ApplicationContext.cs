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
    public DbSet<Product> Products { get; set; }
    public DbSet<Certificate> Certificates { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }
    public DbSet<Partner> Partners { get; set; }
    public DbSet<Specialist> Specialists { get; set; }
    public DbSet<State> States { get; set; }
}