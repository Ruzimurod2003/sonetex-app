using Microsoft.EntityFrameworkCore;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Repositories;
public interface IManufacturerRepository
{
    List<Manufacturer> GetManufacturers(string currentCultureName);
}
public class ManufacturerRepository : IManufacturerRepository
{
    private readonly ApplicationContext _context;

    public ManufacturerRepository(ApplicationContext context)
    {
        _context = context;
    }
    public List<Manufacturer> GetManufacturers(string currentCultureName)
    {
        List<Manufacturer> manufacturers = new List<Manufacturer>();
        var dbManufacturers = _context.Manufacturers
                                    .Include(i => i.Image)
                                    .Include(i => i.Catalogs)
                                    .ThenInclude(i => i.Products)
                                    .ToList();

        if (currentCultureName == "uz")
        {
            foreach (var dbManufacturer in dbManufacturers)
            {
                var manufacturer = new Manufacturer();
                manufacturer.Id = dbManufacturer.Id;
                manufacturer.Name = dbManufacturer.NameUzbek;
                manufacturer.Description = dbManufacturer.DescriptionUzbek;
                manufacturer.Image = dbManufacturer.Image;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog()
                {
                    Id = i.Id,
                    Name = i.NameUzbek,
                    Description = i.DescriptionUzbek,
                    Products = i.Products
                }).ToList();

                manufacturers.Add(manufacturer);
            }
        }
        else if (currentCultureName == "ru")
        {
            foreach (var dbManufacturer in dbManufacturers)
            {
                var manufacturer = new Manufacturer();
                manufacturer.Id = dbManufacturer.Id;
                manufacturer.Name = dbManufacturer.NameRussian;
                manufacturer.Description = dbManufacturer.DescriptionRussian;
                manufacturer.Image = dbManufacturer.Image;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog()
                {
                    Id = i.Id,
                    Name = i.NameRussian,
                    Description = i.DescriptionRussian,
                    Products = i.Products
                }).ToList();

                manufacturers.Add(manufacturer);
            }
        }
        else if (currentCultureName == "en")
        {
            foreach (var dbManufacturer in dbManufacturers)
            {
                var manufacturer = new Manufacturer();
                manufacturer.Id = dbManufacturer.Id;
                manufacturer.Name = dbManufacturer.NameEnglish;
                manufacturer.Description = dbManufacturer.DescriptionEnglish;
                manufacturer.Image = dbManufacturer.Image;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog()
                {
                    Id = i.Id,
                    Name = i.NameEnglish,
                    Description = i.DescriptionEnglish,
                    Products = i.Products
                }).ToList();

                manufacturers.Add(manufacturer);
            }
        }
        else
        {
            foreach (var dbManufacturer in dbManufacturers)
            {
                var manufacturer = new Manufacturer();
                manufacturer.Id = dbManufacturer.Id;
                manufacturer.Name = dbManufacturer.Name;
                manufacturer.Description = dbManufacturer.Description;
                manufacturer.Image = dbManufacturer.Image;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog()
                {
                    Id = i.Id,
                    Name = i.Name,
                    Description = i.Description,
                    Products = i.Products
                }).ToList();

                manufacturers.Add(manufacturer);
            }
        }

        return manufacturers;
    }
}