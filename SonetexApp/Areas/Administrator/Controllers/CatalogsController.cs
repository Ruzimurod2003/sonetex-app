using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
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
        public async Task<IActionResult> Index(string searchString = null, int page = 1)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            AdministratorIndexCatalogVM viewModel = new AdministratorIndexCatalogVM();
            var catalogs = new List<Catalog>();
            if (string.IsNullOrEmpty(searchString))
            {
                catalogs = await _context.Catalogs.ToListAsync();
            }
            else
            {
                catalogs.AddRange(await _context.Catalogs.Where(i => i.Name.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.NameRussian.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.NameUzbek.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.NameEnglish.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.Description.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.DescriptionRussian.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.DescriptionUzbek.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.DescriptionEnglish.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.Id.ToString().ToUpper().Contains(searchString.ToUpper())).ToListAsync());
            }
            catalogs = catalogs.Distinct().ToList();

            int pageSize = 50; // количество объектов на страницу
            List<Catalog> catalogsPerPages = catalogs
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = catalogs.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Catalogs = catalogsPerPages;
            viewModel.SearchString = searchString;

            return View(viewModel);
        }
        // POST: Administrator/Catalogs
        [HttpPost]
        public async Task<IActionResult> Index(AdministratorIndexCatalogVM viewModel)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var catalogs = new List<Catalog>();

            if (string.IsNullOrEmpty(viewModel.SearchString))
            {
                catalogs = await _context.Catalogs.ToListAsync();
            }
            else
            {
                catalogs.AddRange(await _context.Catalogs.Where(i => i.Name.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.NameRussian.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.NameUzbek.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.NameEnglish.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.Description.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.DescriptionRussian.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.DescriptionUzbek.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.DescriptionEnglish.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                catalogs.AddRange(await _context.Catalogs.Where(i => i.Id.ToString().ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
            }

            catalogs = catalogs.Distinct().ToList();
            int page = 1;
            int pageSize = 50; // количество объектов на страницу
            List<Catalog> catalogsPerPage = catalogs
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = catalogs.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Catalogs = catalogsPerPage;

            return View(viewModel);
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
                catalog.Name = catalogVM.Catalog.Name;
                catalog.NameRussian = catalogVM.Catalog.NameRussian;
                catalog.NameEnglish = catalogVM.Catalog.NameEnglish;
                catalog.NameUzbek = catalogVM.Catalog.NameUzbek;
                catalog.Description = catalogVM.Catalog.Description;
                catalog.DescriptionUzbek = catalogVM.Catalog.DescriptionUzbek;
                catalog.DescriptionRussian = catalogVM.Catalog.DescriptionRussian;
                catalog.DescriptionEnglish = catalogVM.Catalog.DescriptionEnglish;

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

            var catalogVM = new AdministratorCatalogToManufacturerVM();
            catalogVM.CatalogName = catalog.Name;
            catalogVM.CatalogId = catalog.Id;
            catalogVM.ManufaturerIds = catalog.Manufacturers.Select(i => i.Id).ToList();
            catalogVM.AllManufaturers = _context.Manufacturers.ToList();

            return View(catalogVM);
        }

        // POST: Administrator/Catalogs/AddManufacturer/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddManufacturer(int id, AdministratorCatalogToManufacturerVM catalogVM)
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
