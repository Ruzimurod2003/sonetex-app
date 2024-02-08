using Microsoft.EntityFrameworkCore;
using SonetexApp.Models;

namespace SonetexApp.Data;

public class ApplicationContext : DbContext
{
    public List<Catalog> Catalogs { get; set; }
    public List<Product> Products { get; set; }
    public List<Certificate> Certificates { get; set; }
    public List<Manufacturer> Manufacturers { get; set; }
    public List<Partner> Partners { get; set; }
    public List<Recommendation> Recommendations { get; set; }
    public List<Specialist> Specialists { get; set; }
    public List<State> States { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source = SonetexDB.db");
    }
}