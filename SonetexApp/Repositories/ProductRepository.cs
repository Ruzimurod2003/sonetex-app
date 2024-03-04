using Microsoft.EntityFrameworkCore;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Repositories;
public interface IProductRepository
{
    List<Product> GetProducts(string currentCultureName);
    Product GetProductById(string currentCultureName, int productId);
}
public class ProductRepository : IProductRepository
{
    private readonly ApplicationContext _context;

    public ProductRepository(ApplicationContext context)
    {
        _context = context;
    }

    public Product GetProductById(string currentCultureName, int productId)
    {
        List<Product> products = new List<Product>();
        var dbProducts = _context.Products
                            .Include(i => i.Images)
                            .Include(i => i.State)
                            .Include(i => i.Type)
                            .Include(i => i.Catalog)
                            .ThenInclude(i => i.Manufacturers)
                            .ToList();
        var product = new Product();

        if (currentCultureName == "uz")
        {
            var dbProduct = dbProducts.Where(i => i.Id == productId).FirstOrDefault();
            product.Id = dbProduct.Id;
            product.Name = dbProduct.NameUzbek;
            product.Description = dbProduct.DescriptionUzbek;
            product.VendorCode = dbProduct.VendorCode;
            product.Availability = dbProduct.Availability;
            product.Guarantee = dbProduct.Guarantee;
            product.Catalog = new Catalog()
            {
                Id = dbProduct.CatalogId,
                Name = dbProduct.Catalog.NameUzbek,
                Description = dbProduct.Catalog.DescriptionUzbek
            };
            product.Type = new Models.Type()
            {
                Id = dbProduct.TypeId,
                Name = dbProduct.Type.NameUzbek
            };
            product.State = new State()
            {
                Id = dbProduct.StateId,
                Name = dbProduct.State.NameUzbek
            };
            product.Images = dbProduct.Images;
        }
        else if (currentCultureName == "ru")
        {
            var dbProduct = dbProducts.Where(i => i.Id == productId).FirstOrDefault();
            product.Id = dbProduct.Id;
            product.Name = dbProduct.NameRussian;
            product.Description = dbProduct.DescriptionRussian;
            product.VendorCode = dbProduct.VendorCode;
            product.Availability = dbProduct.Availability;
            product.Guarantee = dbProduct.Guarantee;
            product.Catalog = new Catalog()
            {
                Id = dbProduct.CatalogId,
                Name = dbProduct.Catalog.NameRussian,
                Description = dbProduct.Catalog.DescriptionRussian
            };
            product.Type = new Models.Type()
            {
                Id = dbProduct.TypeId,
                Name = dbProduct.Type.NameRussian
            };
            product.State = new State()
            {
                Id = dbProduct.StateId,
                Name = dbProduct.State.NameRussian
            };
            product.Images = dbProduct.Images;
        }
        else if (currentCultureName == "en")
        {
            var dbProduct = dbProducts.Where(i => i.Id == productId).FirstOrDefault();
            product.Id = dbProduct.Id;
            product.Name = dbProduct.NameEnglish;
            product.Description = dbProduct.DescriptionEnglish;
            product.VendorCode = dbProduct.VendorCode;
            product.Availability = dbProduct.Availability;
            product.Guarantee = dbProduct.Guarantee;
            product.Catalog = new Catalog()
            {
                Id = dbProduct.CatalogId,
                Name = dbProduct.Catalog.NameEnglish,
                Description = dbProduct.Catalog.DescriptionEnglish
            };
            product.Type = new Models.Type()
            {
                Id = dbProduct.TypeId,
                Name = dbProduct.Type.NameEnglish
            };
            product.State = new State()
            {
                Id = dbProduct.StateId,
                Name = dbProduct.State.NameEnglish
            };
            product.Images = dbProduct.Images;
        }
        else
        {
            var dbProduct = dbProducts.Where(i => i.Id == productId).FirstOrDefault();
            product.Id = dbProduct.Id;
            product.Name = dbProduct.Name;
            product.Description = dbProduct.Description;
            product.VendorCode = dbProduct.VendorCode;
            product.Availability = dbProduct.Availability;
            product.Guarantee = dbProduct.Guarantee;
            product.Catalog = new Catalog()
            {
                Id = dbProduct.CatalogId,
                Name = dbProduct.Catalog.Name,
                Description = dbProduct.Catalog.Description
            };
            product.Type = new Models.Type()
            {
                Id = dbProduct.TypeId,
                Name = dbProduct.Type.Name
            };
            product.State = new State()
            {
                Id = dbProduct.StateId,
                Name = dbProduct.State.Name
            };
            product.Images = dbProduct.Images;
        }
        return product;
    }

    public List<Product> GetProducts(string currentCultureName)
    {
        List<Product> products = new List<Product>();
        var dbProducts = _context.Products
                            .Include(i => i.Images)
                            .Include(i => i.State)
                            .Include(i => i.Type)
                            .Include(i => i.Catalog)
                            .ThenInclude(i => i.Manufacturers)
                            .ToList();

        if (currentCultureName == "uz")
        {
            foreach (var dbProduct in dbProducts)
            {
                var product = new Product();
                product.Id = dbProduct.Id;
                product.Name = dbProduct.NameUzbek;
                product.Description = dbProduct.DescriptionUzbek;
                product.VendorCode = dbProduct.VendorCode;
                product.Availability = dbProduct.Availability;
                product.Guarantee = dbProduct.Guarantee;
                product.Catalog = new Catalog()
                {
                    Id = dbProduct.CatalogId,
                    Name = dbProduct.Catalog.NameUzbek,
                    Description = dbProduct.Catalog.DescriptionUzbek
                };
                product.Type = new Models.Type()
                {
                    Id = dbProduct.TypeId,
                    Name = dbProduct.Type.NameUzbek
                };
                product.State = new State()
                {
                    Id = dbProduct.StateId,
                    Name = dbProduct.State.NameUzbek
                };
                product.Images = dbProduct.Images;

                products.Add(product);
            }
        }
        else if (currentCultureName == "ru")
        {
            foreach (var dbProduct in dbProducts)
            {
                var product = new Product();
                product.Id = dbProduct.Id;
                product.Name = dbProduct.NameRussian;
                product.Description = dbProduct.DescriptionRussian;
                product.VendorCode = dbProduct.VendorCode;
                product.Availability = dbProduct.Availability;
                product.Guarantee = dbProduct.Guarantee;
                product.Catalog = new Catalog()
                {
                    Id = dbProduct.CatalogId,
                    Name = dbProduct.Catalog.NameRussian,
                    Description = dbProduct.Catalog.DescriptionRussian
                };
                product.Type = new Models.Type()
                {
                    Id = dbProduct.TypeId,
                    Name = dbProduct.Type.NameRussian
                };
                product.State = new State()
                {
                    Id = dbProduct.StateId,
                    Name = dbProduct.State.NameRussian
                };
                product.Images = dbProduct.Images;

                products.Add(product);
            }
        }
        else if (currentCultureName == "en")
        {
            foreach (var dbProduct in dbProducts)
            {
                var product = new Product();
                product.Id = dbProduct.Id;
                product.Name = dbProduct.NameEnglish;
                product.Description = dbProduct.DescriptionEnglish;
                product.VendorCode = dbProduct.VendorCode;
                product.Availability = dbProduct.Availability;
                product.Guarantee = dbProduct.Guarantee;
                product.Catalog = new Catalog()
                {
                    Id = dbProduct.CatalogId,
                    Name = dbProduct.Catalog.NameEnglish,
                    Description = dbProduct.Catalog.DescriptionEnglish
                };
                product.Type = new Models.Type()
                {
                    Id = dbProduct.TypeId,
                    Name = dbProduct.Type.NameEnglish
                };
                product.State = new State()
                {
                    Id = dbProduct.StateId,
                    Name = dbProduct.State.NameEnglish
                };
                product.Images = dbProduct.Images;

                products.Add(product);
            }
        }
        else
        {
            foreach (var dbProduct in dbProducts)
            {
                var product = new Product();
                product.Id = dbProduct.Id;
                product.Name = dbProduct.Name;
                product.Description = dbProduct.Description;
                product.VendorCode = dbProduct.VendorCode;
                product.Availability = dbProduct.Availability;
                product.Guarantee = dbProduct.Guarantee;
                product.Catalog = new Catalog()
                {
                    Id = dbProduct.CatalogId,
                    Name = dbProduct.Catalog.Name,
                    Description = dbProduct.Catalog.Description
                };
                product.Type = new Models.Type()
                {
                    Id = dbProduct.TypeId,
                    Name = dbProduct.Type.Name
                };
                product.State = new State()
                {
                    Id = dbProduct.StateId,
                    Name = dbProduct.State.Name
                };
                product.Images = dbProduct.Images;

                products.Add(product);
            }
        }
        return products;
    }
}