using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class PartnersController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public PartnersController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Partners
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Partners.Include(p => p.Image);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Administrator/Partners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // GET: Administrator/Partners/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Partners/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorPartnerVM partnerVM)
        {
            if (ModelState.IsValid)
            {
                // путь к папке Files
                string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                // сохраняем файл в папку Files в каталоге wwwroot
                using (var fileStream = new FileStream(Path.Combine(path, partnerVM.File.FileName), FileMode.Create))
                {
                    await partnerVM.File.CopyToAsync(fileStream);
                }

                Models.File file = new Models.File { Name = partnerVM.File.FileName, Path = path, Description = partnerVM.FileCaption };
                _context.Files.Add(file);
                _context.SaveChanges();

                partnerVM.Partner.Image = file;

                _context.Partners.Add(partnerVM.Partner);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(partnerVM);
        }

        // GET: Administrator/Partners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            var partnerVM = new AdministratorPartnerVM();
            partnerVM.Partner = partner;
            partnerVM.FileCaption = partner.Image.Description;

            return View(partnerVM);
        }

        // POST: Administrator/Partners/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,AdministratorPartnerVM partnerVM)
        {
            if (id != partnerVM.Partner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var partner = await _context.Partners.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                int imageId = partner.Image.Id;
                partner = partnerVM.Partner;

                if (partnerVM.File is not null)
                {
                    // путь к папке Files
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(Path.Combine(path, partnerVM.File.FileName), FileMode.Create))
                    {
                        await partnerVM.File.CopyToAsync(fileStream);
                    }

                    Models.File file = new Models.File { Name = partnerVM.File.FileName, Path = path, Description = partnerVM.FileCaption };
                    _context.Files.Add(file);
                    _context.SaveChanges();

                    partner.Image = file;
                }
                else
                {
                    Models.File file = _context.Files.FirstOrDefault(i => i.Id == imageId);
                    file.Description = partnerVM.FileCaption;

                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(partnerVM);
        }

        // GET: Administrator/Partners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var partner = await _context.Partners
                .Include(p => p.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (partner == null)
            {
                return NotFound();
            }

            return View(partner);
        }

        // POST: Administrator/Partners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var partner = await _context.Partners.FindAsync(id);
            if (partner != null)
            {
                _context.Partners.Remove(partner);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PartnerExists(int id)
        {
            return _context.Partners.Any(e => e.Id == id);
        }
    }
}
