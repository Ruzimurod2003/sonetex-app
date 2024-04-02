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
    public DbSet<Team> Teams { get; set; }
    public DbSet<State> States { get; set; }
    public DbSet<MProduct> MProducts { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Culture>().HasData(
            new List<Culture>()
            {
                new Culture() { Id = 1, Name = "en" },
                new Culture() { Id = 2, Name = "ru" },
                new Culture() { Id = 3, Name = "uz" }
            });

        modelBuilder.Entity<Configuration>().HasData(
            new List<Configuration>()
            {
                new Configuration() {
                    Id = 1,
                    Address = "National Bank of Uzbekistan, Abdulla Qodiriy ko'chasi 1, 100019, Тоshkent, Toshkent",
                    YoutubeLink = "https://youtu.be/8bPgK1feHuE?si=DXRHcaL8fVvpG3Sb",
                    Email = "ruzimurodabdunazarov2003@mail.ru",
                    TwitterLink = "https://twitter.com/elonmusk",
                    FacebookLink = "https://www.facebook.com/Microsoft",
                    GithubLink = "https://github.com/Ruzimurod2003",
                    GoogleLink = "https://www.google.com/",
                    InstagramLink = "https://www.instagram.com/kamelyamelnicofficial/",
                    TelegramLink = "https://t.me/reiiz_d",
                    PhoneNumber = "+998 97 537 84 72"
                }
            });
    }
}