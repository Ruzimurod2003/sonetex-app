using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using NuGet.Protocol.Core.Types;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Commons;
using System.Security.Claims;
using Telegram.Bot.Types;
using SonetexApp.Models;
using SonetexApp.Data;
using Microsoft.EntityFrameworkCore;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ApplicationContext _context;

        public HomeController(IConfiguration configuration, ApplicationContext context)
        {
            _configuration = configuration;
            _context = context;
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
        public async Task<ActionResult> ContactUs()
        {
            ViewBag.MessageResult = false;
            var viewModel = new MainHomeContactUsVM();
            return View(viewModel);
        }
        [HttpPost]
        public async Task<ActionResult> ContactUsAsync(MainHomeContactUsVM viewModel)
        {
            if (ModelState.IsValid)
            {
                string token = _configuration.GetValue<string>("Telegram:Token");
                string adminId = _configuration.GetValue<string>("Telegram:AdminId");
                await Functions.SendMessageFromTelegramAsync(token, adminId, viewModel.Name, viewModel.Email, viewModel.Subject, viewModel.Message);

                viewModel = new MainHomeContactUsVM();
                ViewBag.MessageResult = true;
                return Redirect(nameof(Index));
            }
            return View(viewModel);
        }
        public IActionResult Login()
        {
            MainHomeLoginVM viewModel = new MainHomeLoginVM();
            viewModel.IsAuthenticated = true;
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(MainHomeLoginVM viewModel)
        {
            if (ModelState.IsValid)
            {
                var result = (((viewModel.Email == _configuration.GetValue<string>("Users:SonetexAdmin:Email")) &&
                                (viewModel.Password == _configuration.GetValue<string>("Users:SonetexAdmin:Password"))) ||
                                    ((viewModel.Email == _configuration.GetValue<string>("Users:DurrizoAdmin:Email")) &&
                                        (viewModel.Password == _configuration.GetValue<string>("Users:DurrizoAdmin:Password"))));

                if (result)
                {
                    await Authenticate(viewModel.Email);
                    viewModel.IsAuthenticated = true;

                    return RedirectToAction("Index", "Home", new { Area = "Administrator" });
                }
                viewModel.IsAuthenticated = false;
            }
            return View(viewModel);
        }
        private async Task Authenticate(string email)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, email)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home", new { Area = "Main" });
        }
        public ActionResult Faqs()
        {
            return View();
        }
        public ActionResult PrivacyPolicy()
        {
            return View();
        }
        public ActionResult AboutUs()
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            List<MainHomeAboutUsVM> teams = new List<MainHomeAboutUsVM>();
            var dbTeams = _context.Teams.Include(i => i.Image).ToList();

            if (currentCultureName == "uz")
            {
                foreach (var dbTeam in dbTeams)
                {
                    var team = new MainHomeAboutUsVM();
                    team.Name = dbTeam.NameUzbek;
                    team.Description = dbTeam.DescriptionUzbek;
                    team.ImageName = dbTeam.Image.Name;
                    team.Position = dbTeam.PositionUzbek;
                    team.Phone = dbTeam.Phone;

                    teams.Add(team);
                }
            }
            else if (currentCultureName == "ru")
            {
                foreach (var dbTeam in dbTeams)
                {
                    var team = new MainHomeAboutUsVM();
                    team.Name = dbTeam.NameRussian;
                    team.Description = dbTeam.DescriptionRussian;
                    team.ImageName = dbTeam.Image.Name;
                    team.Position = dbTeam.PositionRussian;
                    team.Phone = dbTeam.Phone;

                    teams.Add(team);
                }
            }
            else if (currentCultureName == "en")
            {
                foreach (var dbTeam in dbTeams)
                {
                    var team = new MainHomeAboutUsVM();
                    team.Name = dbTeam.NameEnglish;
                    team.Description = dbTeam.DescriptionEnglish;
                    team.ImageName = dbTeam.Image.Name;
                    team.Position = dbTeam.PositionEnglish;
                    team.Phone = dbTeam.Phone;

                    teams.Add(team);
                }
            }
            else
            {
                foreach (var dbTeam in dbTeams)
                {
                    var team = new MainHomeAboutUsVM();
                    team.Name = dbTeam.Name;
                    team.Description = dbTeam.Description;
                    team.ImageName = dbTeam.Image.Name;
                    team.Position = dbTeam.Position;
                    team.Phone = dbTeam.Phone;

                    teams.Add(team);
                }
            }

            return View(teams);
        }
    }
}
