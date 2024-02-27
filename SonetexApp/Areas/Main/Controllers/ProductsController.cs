using Microsoft.AspNetCore.Mvc;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
