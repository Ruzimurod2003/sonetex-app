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
    public class CertificatesController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CertificatesController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Certificates
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Certificates.Include(c => c.Document);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Administrator/Certificates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .Include(c => c.Document)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // GET: Administrator/Certificates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Certificates/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorCertificateVM certificateVM)
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
                using (var fileStream = new FileStream(Path.Combine(path, certificateVM.File.FileName), FileMode.Create))
                {
                    await certificateVM.File.CopyToAsync(fileStream);
                }

                Models.File file = new Models.File { Name = certificateVM.File.FileName, Path = path, Description = certificateVM.FileCaption };
                _context.Files.Add(file);
                _context.SaveChanges();

                certificateVM.Certificate.Document = file;

                _context.Certificates.Add(certificateVM.Certificate);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(certificateVM);
        }

        // GET: Administrator/Certificates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates.Include(i => i.Document).FirstOrDefaultAsync(i => i.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            var certificateVM = new AdministratorCertificateVM();
            certificateVM.Certificate = certificate;
            certificateVM.FileCaption = certificate.Document.Description;

            return View(certificateVM);
        }

        // POST: Administrator/Certificates/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorCertificateVM certificateVM)
        {
            if (id != certificateVM.Certificate.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var certificate = await _context.Certificates.Include(i => i.Document).FirstOrDefaultAsync(i => i.Id == id);
                int documentId = certificate.Document.Id;
                certificate = certificateVM.Certificate;

                if (certificateVM.File is not null)
                {
                    // путь к папке Files
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(Path.Combine(path, certificateVM.File.FileName), FileMode.Create))
                    {
                        await certificateVM.File.CopyToAsync(fileStream);
                    }

                    Models.File file = new Models.File { Name = certificateVM.File.FileName, Path = path, Description = certificateVM.FileCaption };
                    _context.Files.Add(file);
                    _context.SaveChanges();

                    certificate.Document = file;
                }
                else
                {
                    Models.File file = _context.Files.FirstOrDefault(i => i.Id == documentId);
                    file.Description = certificateVM.FileCaption;
                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(certificateVM);
        }

        // GET: Administrator/Certificates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var certificate = await _context.Certificates
                .Include(c => c.Document)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (certificate == null)
            {
                return NotFound();
            }

            return View(certificate);
        }

        // POST: Administrator/Certificates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var certificate = await _context.Certificates.FindAsync(id);
            if (certificate != null)
            {
                _context.Certificates.Remove(certificate);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CertificateExists(int id)
        {
            return _context.Certificates.Any(e => e.Id == id);
        }
    }
}
