using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
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
    public class CatalogsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public CatalogsController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Catalogs
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Catalogs;
            return View(await applicationContext.ToListAsync());
        }

        // GET: Administrator/Catalogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .Include(i => i.Manufacturers)
                .ThenInclude(j => j.Catalogs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // GET: Administrator/Catalogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Catalogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorCatalogVM catalogVM)
        {
            if (ModelState.IsValid)
            {
                _context.Catalogs.Add(catalogVM.Catalog);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(catalogVM);
        }

        // GET: Administrator/Catalogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.FirstOrDefaultAsync(i => i.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            var catalogVM = new AdministratorCatalogVM();
            catalogVM.Catalog = catalog;

            return View(catalogVM);
        }

        // POST: Administrator/Catalogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorCatalogVM catalogVM)
        {
            if (id != catalogVM.Catalog.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var catalog = await _context.Catalogs.FirstOrDefaultAsync(i => i.Id == id);
                catalog = catalogVM.Catalog;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(catalogVM);
        }

        // GET: Administrator/Catalogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs
                .FirstOrDefaultAsync(m => m.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            return View(catalog);
        }

        // POST: Administrator/Catalogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var catalog = await _context.Catalogs.FindAsync(id);
            if (catalog != null)
            {
                _context.Catalogs.Remove(catalog);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CatalogExists(int id)
        {
            return _context.Catalogs.Any(e => e.Id == id);
        }
        public async Task<IActionResult> AddManufacturer(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var catalog = await _context.Catalogs.Include(i => i.Manufacturers).FirstOrDefaultAsync(i => i.Id == id);
            if (catalog == null)
            {
                return NotFound();
            }

            var catalogVM = new AdministratorCatalogToManufacturer();
            catalogVM.CatalogName = catalog.Name;
            catalogVM.CatalogId = catalog.Id;
            catalogVM.ManufaturerIds = catalog.Manufacturers.Select(i => i.Id).ToList();
            catalogVM.AllManufaturers = _context.Manufacturers.ToList();

            return View(catalogVM);
        }

        // POST: Administrator/Catalogs/AddManufacturer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddManufacturer(int id, AdministratorCatalogToManufacturer catalogVM)
        {
            if (id != catalogVM.CatalogId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var catalog = await _context.Catalogs.Include(i => i.Manufacturers).FirstOrDefaultAsync(i => i.Id == id);

                var oldManufacturers = catalog.Manufacturers;
                var markedManufacturers = _context.Manufacturers.Where(i => catalogVM.ManufaturerIds.Contains(i.Id)).ToList();
                var removedManufacturers = oldManufacturers.Except(markedManufacturers).ToList();
                var addedManufacturers = markedManufacturers.Except(oldManufacturers).ToList();

                catalog.Manufacturers.AddRange(addedManufacturers);
                foreach (var removedManufacturer in removedManufacturers)
                {
                    catalog.Manufacturers.Remove(removedManufacturer);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = catalog.Id });
            }

            return View(catalogVM);
        }
    }
}
