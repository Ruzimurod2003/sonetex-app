using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class HomeController : Controller
    {
        private readonly ApplicationContext _context;

        public HomeController(ApplicationContext context)
        {
            _context = context;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            var statisticsVM = new AdministratorStatisticsVM();
            statisticsVM.Cultures = _context.Cultures.ToList();
            statisticsVM.Files = _context.Files.ToList();
            statisticsVM.Resources = _context.Resources.ToList();
            statisticsVM.Configurations = _context.Configurations.ToList();
            statisticsVM.Catalogs = _context.Catalogs.ToList();
            statisticsVM.Types = _context.Types.ToList();
            statisticsVM.Products = _context.Products.ToList();
            statisticsVM.Certificates = _context.Certificates.ToList();
            statisticsVM.Manufacturers = _context.Manufacturers.ToList();
            statisticsVM.Partners = _context.Partners.ToList();
            statisticsVM.Specialists = _context.Specialists.ToList();
            statisticsVM.States = _context.States.ToList();

            return View(statisticsVM);
        }
    }
}
