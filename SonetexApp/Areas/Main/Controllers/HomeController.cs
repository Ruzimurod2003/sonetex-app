using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Commons;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        // GET: HomeController
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
        public ActionResult Login()
        {
            return View();
        }
        public async Task<ActionResult> ContactUs()
        {
            ViewBag.MessageResult = false;
            var viewModel = new AdministratorHomeContactUsVM();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> ContactUsAsync(AdministratorHomeContactUsVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string token = _configuration.GetValue<string>("Telegram:Token");
                string adminId = _configuration.GetValue<string>("Telegram:AdminId");
                await Functions.SendMessageFromTelegramAsync(token, adminId, viewModel.Name, viewModel.Email, viewModel.Subject, viewModel.Message);

                viewModel = new AdministratorHomeContactUsVM();
                ViewBag.MessageResult = true;
                return Redirect(nameof(ContactUs));
            }
            return View(viewModel);
        }
    }
}
