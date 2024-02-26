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
    }
}
