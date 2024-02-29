using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class PartnersController : Controller
    {
        private readonly ApplicationContext _context;

        public PartnersController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            List<MainPatrnerVM> partners = new List<MainPatrnerVM>();
            var dbPartners = _context.Partners.Include(i => i.Image).ToList();

            if (currentCultureName == "uz")
            {
                foreach (var dbPartner in dbPartners)
                {
                    var partner = new MainPatrnerVM();
                    partner.Name = dbPartner.NameUzbek;
                    partner.Description = dbPartner.DescriptionUzbek;
                    partner.ImageName = dbPartner.Image.Name;
                    partner.PartnerId = dbPartner.Id;

                    partners.Add(partner);
                }
            }
            else if (currentCultureName == "ru")
            {
                foreach (var dbPartner in dbPartners)
                {
                    var partner = new MainPatrnerVM();
                    partner.Name = dbPartner.NameRussian;
                    partner.Description = dbPartner.DescriptionRussian;
                    partner.ImageName = dbPartner.Image.Name;
                    partner.PartnerId = dbPartner.Id;

                    partners.Add(partner);
                }
            }
            else if (currentCultureName == "en")
            {
                foreach (var dbPartner in dbPartners)
                {
                    var partner = new MainPatrnerVM();
                    partner.Name = dbPartner.NameEnglish;
                    partner.Description = dbPartner.DescriptionEnglish;
                    partner.ImageName = dbPartner.Image.Name;
                    partner.PartnerId = dbPartner.Id;

                    partners.Add(partner);
                }
            }
            else
            {
                foreach (var dbPartner in dbPartners)
                {
                    var partner = new MainPatrnerVM();
                    partner.Name = dbPartner.Name;
                    partner.Description = dbPartner.Description;
                    partner.ImageName = dbPartner.Image.Name;
                    partner.PartnerId = dbPartner.Id;

                    partners.Add(partner);
                }
            }

            return View(partners);
        }
        public IActionResult Details(int id)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var partner = new MainPatrnerVM();
            var dbPartner = _context.Partners.Include(i => i.Image).FirstOrDefault(i => i.Id == id);

            if (currentCultureName == "uz")
            {
                partner.Name = dbPartner.NameUzbek;
                partner.Description = dbPartner.DescriptionUzbek;
                partner.ImageName = dbPartner.Image.Name;
                partner.ImageCaption = dbPartner.Image.Description;
            }
            else if (currentCultureName == "ru")
            {
                partner.Name = dbPartner.NameRussian;
                partner.Description = dbPartner.DescriptionRussian;
                partner.ImageName = dbPartner.Image.Name;
                partner.ImageCaption = dbPartner.Image.Description;
            }
            else if (currentCultureName == "en")
            {
                partner.Name = dbPartner.NameEnglish;
                partner.Description = dbPartner.DescriptionEnglish;
                partner.ImageName = dbPartner.Image.Name;
                partner.ImageCaption = dbPartner.Image.Description;
            }
            else
            {
                partner.Name = dbPartner.Name;
                partner.Description = dbPartner.Description;
                partner.ImageName = dbPartner.Image.Name;
                partner.ImageCaption = dbPartner.Image.Description;
            }

            return View(partner);
        }
    }
}
