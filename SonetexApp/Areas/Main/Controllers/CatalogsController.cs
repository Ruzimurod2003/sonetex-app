using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;
using SonetexApp.Repositories;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class CatalogsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ICatalogRepository _catalogRepository;

        public CatalogsController(ApplicationContext context, ICatalogRepository catalogRepository)
        {
            _context = context;
            _catalogRepository = catalogRepository;
        }
        public IActionResult Index(string firstLetter = null)
        {
            MainCatalogVM viewModel = new MainCatalogVM();

            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var catalogs = _catalogRepository.GetCatalogs(currentCultureName);

            viewModel.Catalogs = (string.IsNullOrEmpty(firstLetter)) ?
                                    catalogs.OrderBy(i => i.Id).ToList() :
                                        catalogs.Where(i => i.Name.Substring(0, 1).ToUpper() == firstLetter.ToUpper()).OrderBy(i => i.Id).ToList();
            viewModel.FirstLetters = catalogs.Select(i => i.Name.Substring(0, 1).ToUpper()).OrderBy(i => i).Distinct().ToList();

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
