using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class CatalogsController : Controller
    {
        private readonly ApplicationContext _context;

        public CatalogsController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            AdministratorCatalogVM viewModel = new AdministratorCatalogVM();

            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            List<Catalog> catalogs = new List<Catalog>();
            var dbCatalogs = _context.Catalogs.Include(i => i.Manufacturers).ToList();

            if (currentCultureName == "uz")
            {
                foreach (var dbCatalog in dbCatalogs)
                {
                    var catalog = new Catalog();
                    catalog.Name = dbCatalog.NameUzbek;
                    catalog.Description = dbCatalog.DescriptionUzbek;
                    catalog.Id = dbCatalog.Id;
                    catalog.Manufacturers = dbCatalog.Manufacturers;

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
                    catalog.Manufacturers = dbCatalog.Manufacturers;

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
                    catalog.Manufacturers = dbCatalog.Manufacturers;

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
                    catalog.Manufacturers = dbCatalog.Manufacturers;

                    catalogs.Add(catalog);
                }
            }
            viewModel.Catalogs = catalogs;

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var catalog = new Catalog();
            var dbCatalog = _context.Catalogs.Include(i => i.Manufacturers).FirstOrDefault(i => i.Id == id);
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            if (currentCultureName == "uz")
            {
                catalog.Name = dbCatalog.NameUzbek;
                catalog.Description = dbCatalog.DescriptionUzbek;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer() { Id = i.Id, Name = i.NameUzbek }).ToList();
            }
            else if (currentCultureName == "ru")
            {
                catalog.Name = dbCatalog.NameRussian;
                catalog.Description = dbCatalog.DescriptionRussian;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer() { Id = i.Id, Name = i.NameRussian }).ToList();
            }
            else if (currentCultureName == "en")
            {
                catalog.Name = dbCatalog.NameEnglish;
                catalog.Description = dbCatalog.DescriptionEnglish;
                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer() { Id = i.Id, Name = i.NameEnglish }).ToList();
            }
            else
            {
                catalog.Name = dbCatalog.Name;
                catalog.Description = dbCatalog.Description;

                catalog.Manufacturers = dbCatalog.Manufacturers.Select(i => new Manufacturer() { Id = i.Id, Name = i.Name }).ToList();
            }
            catalog.Id = dbCatalog.Id;

            return View(catalog);
        }
    }
}
