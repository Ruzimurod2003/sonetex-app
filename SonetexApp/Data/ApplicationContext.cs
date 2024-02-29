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
                new State()
                {
                    Id = 1,
                    Name = "Янги",
                    NameUzbek = "Yangi",
                    NameRussian = "Новый",
                    NameEnglish = "New"
                },
                new State()
                {
                    Id = 2,
                    Name = "Сақлаш жойидан",
                    NameUzbek = "Saqlash joyidan",
                    NameRussian = "С хранения",
                    NameEnglish = "From storage"
                }
            });

        modelBuilder.Entity<Models.Type>().HasData(
            new List<Models.Type>()
            {
                new Models.Type()
                {
                    Id = 1,
                    Name = "Қулай маҳсулотлар",
                    NameUzbek = "Qulay mahsulotlar",
                    NameRussian = "Товары повседневного спроса",
                    NameEnglish = "Convenience products"
                },
                new Models.Type()
                {
                    Id = 2,
                    Name = "Маҳсулотларни харид қилиш",
                    NameUzbek = "Mahsulotlarni xarid qilish",
                    NameRussian = "Товары для покупок",
                    NameEnglish = "Shopping products"
                }
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

        modelBuilder.Entity<Models.File>().HasData(
            new List<Models.File>()
            {
                new Models.File()
                {
                    Id = 1,
                    Name = "omron_manufacture.jpg",
                    Description = "from https://olnisa.ru/"
                },
                new Models.File()
                {
                    Id = 2,
                    Name = "keyence_manufacture.jpg",
                    Description = "from https://olnisa.ru/"
                }
            });

        modelBuilder.Entity<Catalog>().HasData(
            new List<Catalog>()
            {
                new Catalog()
                {
                    Id = 1,
                    Name = "Автоматлаштириш",
                    NameUzbek = "Avtomatlashtirish",
                    NameRussian = "Автоматизация",
                    NameEnglish = "Automation",
                    Description = "Автоматлаштирилган категориялар синфи",
                    DescriptionUzbek = "Avtomatlashtirilgan kategoriyalar sinfi",
                    DescriptionEnglish = "A class of automated categories",
                    DescriptionRussian = "Класс автоматизированных категорий"
                },
                new Catalog()
                {
                    Id = 2,
                    Name = "Аксессуарлар",
                    NameUzbek = "Aksessuarlar",
                    NameRussian = "Аксессуары",
                    NameEnglish = "Accessories",
                    Description = "Аксессуарлар категориялар синфи",
                    DescriptionUzbek = "Aksessuarlar kategoriyalar sinfi",
                    DescriptionEnglish = "Accessory category class",
                    DescriptionRussian = "Категория аксессуаров класс"
                }
            });

        modelBuilder.Entity<Manufacturer>().HasData(
            new List<Manufacturer>()
            {
                new Manufacturer() {
                    Id = 1,
                    Name = "Омрон",
                    NameUzbek = "Omron",
                    NameEnglish = "Omron",
                    NameRussian = "Омрон",
                    Description = "Бу ишлаб чиқарувчи компания телефон махсулотларини етказиб бериш билан ишлайди",
                    DescriptionUzbek = "Bu ishlab chiqaruvchi kompaniya telefon maxsulotlarini yetkazib berish bilan ishlaydi",
                    DescriptionEnglish = "This manufacturing company works with the delivery of phone products",
                    DescriptionRussian = "Данная компания-производитель занимается доставкой телефонной продукции",
                    ImageId = 1
                },

                new Manufacturer() {
                    Id = 2,
                    Name = "Кеенcе",
                    NameUzbek = "Keyence",
                    NameEnglish = "Keyence",
                    NameRussian = "Кеенcе",
                    Description = "Бу ишлаб чиқарувчи компания компютер махсулотларини етказиб бериш билан ишлайди",
                    DescriptionUzbek = "Bu ishlab chiqaruvchi kompaniya kompyuter maxsulotlarini yetkazib berish bilan ishlaydi",
                    DescriptionEnglish = "This manufacturing company deals with the supply of computer products",
                    DescriptionRussian = "Данная производственная компания занимается поставками компьютерной продукции.",
                    ImageId = 2
                }
            });
    }
}