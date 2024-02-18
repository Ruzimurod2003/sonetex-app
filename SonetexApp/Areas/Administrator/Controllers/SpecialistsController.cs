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
    public class SpecialistsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public SpecialistsController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Specialists
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Specialists.Include(s => s.Image);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Administrator/Specialists/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // GET: Administrator/Specialists/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Specialists/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorSpecialistVM specialistVM)
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
                using (var fileStream = new FileStream(Path.Combine(path, specialistVM.File.FileName), FileMode.Create))
                {
                    await specialistVM.File.CopyToAsync(fileStream);
                }

                Models.File file = new Models.File { Name = specialistVM.File.FileName, Path = path, Description = specialistVM.FileCaption };
                _context.Files.Add(file);
                _context.SaveChanges();

                specialistVM.Specialist.Image = file;

                _context.Specialists.Add(specialistVM.Specialist);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(specialistVM);
        }

        // GET: Administrator/Specialists/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            var specialistVM = new AdministratorSpecialistVM();
            specialistVM.Specialist = specialist;
            specialistVM.FileCaption = specialist.Image.Description;

            return View(specialistVM);
        }

        // POST: Administrator/Specialists/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorSpecialistVM specialistVM)
        {
            if (id != specialistVM.Specialist.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var specialist = await _context.Specialists.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                int imageId = specialist.Image.Id;
                specialist = specialistVM.Specialist;

                if (specialistVM.File is not null)
                {
                    // путь к папке Files
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(Path.Combine(path, specialistVM.File.FileName), FileMode.Create))
                    {
                        await specialistVM.File.CopyToAsync(fileStream);
                    }

                    Models.File file = new Models.File { Name = specialistVM.File.FileName, Path = path, Description = specialistVM.FileCaption };
                    _context.Files.Add(file);
                    _context.SaveChanges();

                    specialist.Image = file;
                }
                else
                {
                    Models.File file = _context.Files.FirstOrDefault(i => i.Id == imageId);
                    file.Description = specialistVM.FileCaption;

                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(specialistVM);
        }

        // GET: Administrator/Specialists/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specialist = await _context.Specialists
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (specialist == null)
            {
                return NotFound();
            }

            return View(specialist);
        }

        // POST: Administrator/Specialists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specialist = await _context.Specialists.FindAsync(id);
            if (specialist != null)
            {
                _context.Specialists.Remove(specialist);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SpecialistExists(int id)
        {
            return _context.Specialists.Any(e => e.Id == id);
        }
    }
}
