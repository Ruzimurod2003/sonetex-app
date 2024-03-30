using Microsoft.EntityFrameworkCore;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Repositories;
public interface ICatalogRepository
{
    List<Catalog> GetCatalogs(string currentCultureName);
    List<Catalog> GetCatalogs(string currentCultureName, int count);
}
public class CatalogRepository : ICatalogRepository
{
    private readonly ApplicationContext _context;

    public CatalogRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<Catalog> GetCatalogs(string currentCultureName)
    {
        List<Catalog> catalogs = new List<Catalog>();
        var dbCatalogs = _context.Catalogs
                            .Include(i => i.Manufacturers)
                            .Include(i => i.Products)
                            .ToList();

        if (currentCultureName == "uz")
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.NameUzbek;
                catalog.Description = dbCatalog.DescriptionUzbek;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.NameUzbek,
                    Description = dbCatalog.DescriptionUzbek,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        else if (currentCultureName == "ru")
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.NameRussian;
                catalog.Description = dbCatalog.DescriptionRussian;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.NameRussian,
                    Description = dbCatalog.DescriptionRussian,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        else if (currentCultureName == "en")
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.NameEnglish;
                catalog.Description = dbCatalog.DescriptionEnglish;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.NameEnglish,
                    Description = dbCatalog.DescriptionEnglish,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        else
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.Name;
                catalog.Description = dbCatalog.Description;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = dbCatalog.Description,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        return catalogs;
    }

    public List<Catalog> GetCatalogs(string currentCultureName, int count)
    {
        List<Catalog> catalogs = new List<Catalog>();
        var dbCatalogs = _context.Catalogs
                            .Include(i => i.Manufacturers)
                            .Include(i => i.Products)
                            .Take(count)
                            .ToList();

        if (currentCultureName == "uz")
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.NameUzbek;
                catalog.Description = dbCatalog.DescriptionUzbek;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.NameUzbek,
                    Description = dbCatalog.DescriptionUzbek,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        else if (currentCultureName == "ru")
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.NameRussian;
                catalog.Description = dbCatalog.DescriptionRussian;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.NameRussian,
                    Description = dbCatalog.DescriptionRussian,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        else if (currentCultureName == "en")
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.NameEnglish;
                catalog.Description = dbCatalog.DescriptionEnglish;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.NameEnglish,
                    Description = dbCatalog.DescriptionEnglish,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        else
        {
            foreach (var dbCatalog in dbCatalogs)
            {
                var catalog = new Catalog();
                catalog.Name = dbCatalog.Name;
                catalog.Description = dbCatalog.Description;
                catalog.Id = dbCatalog.Id;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = dbCatalog.Description,
                    ImageId = i.ImageId,
                    Image = i.Image
                }).ToList();
                catalog.Products = dbCatalog.Products;

                catalogs.Add(catalog);
            }
        }
        return catalogs;
    }
}