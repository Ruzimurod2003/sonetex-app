using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class SpecialistsController : Controller
    {
        private readonly ApplicationContext _context;

        public SpecialistsController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            List<AdministratorSpecialistVM> specialists = new List<AdministratorSpecialistVM>();
            var dbSpecialists = _context.Specialists.Include(i => i.Image).ToList();

            if (currentCultureName == "uz")
            {
                foreach (var dbSpecialist in dbSpecialists)
                {
                    var specialist = new AdministratorSpecialistVM();
                    specialist.Name = dbSpecialist.NameUzbek;
                    specialist.Description = dbSpecialist.DescriptionUzbek;
                    specialist.ImageName = dbSpecialist.Image.Name;
                    specialist.SpecialistId = dbSpecialist.Id;

                    specialists.Add(specialist);
                }
            }
            else if (currentCultureName == "ru")
            {
                foreach (var dbSpecialist in dbSpecialists)
                {
                    var specialist = new AdministratorSpecialistVM();
                    specialist.Name = dbSpecialist.NameRussian;
                    specialist.Description = dbSpecialist.DescriptionRussian;
                    specialist.ImageName = dbSpecialist.Image.Name;
                    specialist.SpecialistId = dbSpecialist.Id;

                    specialists.Add(specialist);
                }
            }
            else if (currentCultureName == "en")
            {
                foreach (var dbSpecialist in dbSpecialists)
                {
                    var specialist = new AdministratorSpecialistVM();
                    specialist.Name = dbSpecialist.NameEnglish;
                    specialist.Description = dbSpecialist.DescriptionEnglish;
                    specialist.ImageName = dbSpecialist.Image.Name;
                    specialist.SpecialistId = dbSpecialist.Id;

                    specialists.Add(specialist);
                }
            }
            else
            {
                foreach (var dbSpecialist in dbSpecialists)
                {
                    var specialist = new AdministratorSpecialistVM();
                    specialist.Name = dbSpecialist.Name;
                    specialist.Description = dbSpecialist.Description;
                    specialist.ImageName = dbSpecialist.Image.Name;
                    specialist.SpecialistId = dbSpecialist.Id;

                    specialists.Add(specialist);
                }
            }

            return View(specialists);
        }
        public IActionResult Details(int id)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var specialist = new AdministratorSpecialistVM();
            var dbSpecialist = _context.Specialists.Include(i => i.Image).FirstOrDefault(i => i.Id == id);

            if (currentCultureName == "uz")
            {
                specialist.Name = dbSpecialist.NameUzbek;
                specialist.Description = dbSpecialist.DescriptionUzbek;
                specialist.ImageName = dbSpecialist.Image.Name;
                specialist.ImageCaption = dbSpecialist.Image.Description;
            }
            else if (currentCultureName == "ru")
            {
                specialist.Name = dbSpecialist.NameRussian;
                specialist.Description = dbSpecialist.DescriptionRussian;
                specialist.ImageName = dbSpecialist.Image.Name;
                specialist.ImageCaption = dbSpecialist.Image.Description;
            }
            else if (currentCultureName == "en")
            {
                specialist.Name = dbSpecialist.NameEnglish;
                specialist.Description = dbSpecialist.DescriptionEnglish;
                specialist.ImageName = dbSpecialist.Image.Name;
                specialist.ImageCaption = dbSpecialist.Image.Description;
            }
            else
            {
                specialist.Name = dbSpecialist.Name;
                specialist.Description = dbSpecialist.Description;
                specialist.ImageName = dbSpecialist.Image.Name;
                specialist.ImageCaption = dbSpecialist.Image.Description;
            }

            return View(specialist);
        }
    }
}
