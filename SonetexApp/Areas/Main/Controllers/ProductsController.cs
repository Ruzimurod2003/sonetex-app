using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;
using SonetexApp.Repositories;
using System.Drawing.Printing;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly ICatalogRepository _catalogRepository;
        private readonly IManufacturerRepository _manufacturerRepository;
        private readonly IStateRepository _stateRepository;
        private readonly ITypeRepository _typeRepository;
        private readonly IProductRepository _productRepository;

        public ProductsController(
            ApplicationContext context,
            ICatalogRepository catalogRepository,
            IManufacturerRepository manufacturerRepository,
            IStateRepository stateRepository,
            ITypeRepository typeRepository,
            IProductRepository productRepository
            )
        {
            _context = context;
            _catalogRepository = catalogRepository;
            _manufacturerRepository = manufacturerRepository;
            _stateRepository = stateRepository;
            _typeRepository = typeRepository;
            _productRepository = productRepository;
        }
        public IActionResult Index(int page = 1)
        {
            MainProductVM viewModel = new MainProductVM();
            viewModel.CatalogIds = new List<int>();
            viewModel.ManufacturerIds = new List<int>();
            viewModel.StateIds = new List<int>();
            viewModel.TypeIds = new List<int>();

            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

            viewModel.Catalogs = _catalogRepository.GetCatalogs(currentCultureName);
            viewModel.Manufacturers = _manufacturerRepository.GetManufacturers(currentCultureName);
            viewModel.States = _stateRepository.GetStates(currentCultureName);
            viewModel.Types = _typeRepository.GetTypes(currentCultureName);
            var products = _productRepository.GetProducts(currentCultureName);

            int pageSize = 3; // количество объектов на страницу
            List<Product> productsPerPages = products
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = products.Count
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Products = productsPerPages;
            viewModel.ProductsCount = products.Count;

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(MainProductVM viewModel, int page = 1)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

            viewModel.Catalogs = _catalogRepository.GetCatalogs(currentCultureName);
            viewModel.Manufacturers = _manufacturerRepository.GetManufacturers(currentCultureName);
            viewModel.States = _stateRepository.GetStates(currentCultureName);
            viewModel.Types = _typeRepository.GetTypes(currentCultureName);
            var products = new List<Product>();
            var allProducts = _productRepository.GetProducts(currentCultureName);

            if (viewModel.TypeIds != null && viewModel.TypeIds.Any())
            {
                products.AddRange(allProducts.Where(i => viewModel.TypeIds.Contains(i.Type.Id)));
            }
            else
            {
                viewModel.TypeIds = new List<int>();
            }

            if (viewModel.StateIds != null && viewModel.StateIds.Any())
            {
                products.AddRange(allProducts.Where(i => viewModel.StateIds.Contains(i.State.Id)));
            }
            else
            {
                viewModel.StateIds = new List<int>();
            }

            if (viewModel.CatalogIds != null && viewModel.CatalogIds.Any())
            {
                products.AddRange(allProducts.Where(i => viewModel.CatalogIds.Contains(i.Catalog.Id)));
            }
            else
            {
                viewModel.CatalogIds = new List<int>();
            }

            if (viewModel.ManufacturerIds != null && viewModel.ManufacturerIds.Any())
            {
                List<int> catalogIds = new List<int>();
                foreach (var manufacturerId in viewModel.ManufacturerIds)
                {
                    catalogIds.AddRange(viewModel.Manufacturers
                                        .Where(i => i.Id == manufacturerId)
                                        .FirstOrDefault()
                                        .Catalogs
                                        .Select(j => j.Id)
                                        .ToList());
                }
                products.AddRange(allProducts.Where(i => catalogIds.Contains(i.Catalog.Id)));
            }
            else
            {
                viewModel.ManufacturerIds = new List<int>();
            }

            if (!string.IsNullOrEmpty(viewModel.SearchProduct))
            {
                products.AddRange(allProducts.Where(i => i.Name.Contains(viewModel.SearchProduct)).ToList());
                products.AddRange(allProducts.Where(i => i.Description.Contains(viewModel.SearchProduct)).ToList());
            }
            else
            {
                viewModel.SearchProduct = string.Empty;
            }

            viewModel.Products = products.DistinctBy(i => i.Id).ToList();

            int pageSize = 1; // количество объектов на страницу
            List<Product> productsPerPages = products
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = products.Count
            };
            viewModel.PageInfo = pageInfo;

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;

            var product = _productRepository.GetProductById(currentCultureName, id);

            return View(product);
        }
    }
}
