using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;
using SonetexApp.Repositories;

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
        public IActionResult Index()
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
            viewModel.Products = _productRepository.GetProducts(currentCultureName);

            return View(viewModel);
        }
        [HttpPost]
        public IActionResult Index(MainProductVM viewModel)
        {
            return View(viewModel);
        }
    }
}
