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
        public IActionResult Index(int page = 1)
        {
            MainCatalogVM viewModel = new MainCatalogVM();

            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var catalogs = _catalogRepository.GetCatalogs(currentCultureName);

            int pageSize = 6; // количество объектов на страницу
            List<Catalog> catalogsPerPages = catalogs
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = catalogs.Count
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Catalogs = catalogsPerPages;
            viewModel.CatalogsCount = catalogs.Count;

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
