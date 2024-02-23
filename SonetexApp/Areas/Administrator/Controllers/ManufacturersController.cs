using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class ManufacturersController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public ManufacturersController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Manufacturers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Manufacturers.ToListAsync());
        }

        // GET: Administrator/Manufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers
                .Include(i => i.File)
                .Include(i => i.Catalogs)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // GET: Administrator/Manufacturers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Manufacturers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorManufacturerVM manufacturerVM)
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
                using (var fileStream = new FileStream(Path.Combine(path, manufacturerVM.File.FileName), FileMode.Create))
                {
                    await manufacturerVM.File.CopyToAsync(fileStream);
                }

                Models.File file = new Models.File { Name = manufacturerVM.File.FileName, Path = path, Description = manufacturerVM.FileCaption };
                _context.Files.Add(file);
                _context.SaveChanges();

                manufacturerVM.Manufacturer.File = file;

                _context.Manufacturers.Add(manufacturerVM.Manufacturer);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(manufacturerVM);
        }

        // GET: Administrator/Manufacturers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers.Include(i => i.File).FirstOrDefaultAsync(i => i.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            var manufacturerVM = new AdministratorManufacturerVM();
            manufacturerVM.Manufacturer = manufacturer;
            manufacturerVM.FileCaption = manufacturer.File.Description;

            return View(manufacturerVM);
        }

        // POST: Administrator/Manufacturers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorManufacturerVM manufacturerVM)
        {
            if (id != manufacturerVM.Manufacturer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var manufacturer = await _context.Manufacturers.Include(i => i.File).FirstOrDefaultAsync(i => i.Id == id);
                int imageId = manufacturer.File.Id;
                manufacturer = manufacturerVM.Manufacturer;

                if (manufacturerVM.File is not null)
                {
                    // путь к папке Files
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(Path.Combine(path, manufacturerVM.File.FileName), FileMode.Create))
                    {
                        await manufacturerVM.File.CopyToAsync(fileStream);
                    }

                    Models.File file = new Models.File { Name = manufacturerVM.File.FileName, Path = path, Description = manufacturerVM.FileCaption };
                    _context.Files.Add(file);
                    _context.SaveChanges();
                    manufacturer.File = file;
                }
                else
                {
                    Models.File file = _context.Files.FirstOrDefault(i => i.Id == imageId);
                    file.Description = manufacturerVM.FileCaption;
                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(manufacturerVM);
        }

        // GET: Administrator/Manufacturers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers
                .Include(i => i.File)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            return View(manufacturer);
        }

        // POST: Administrator/Manufacturers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ManufacturerExists(int id)
        {
            return _context.Manufacturers.Any(e => e.Id == id);
        }
        public async Task<IActionResult> AddCatalog(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers.Include(i => i.Catalogs).FirstOrDefaultAsync(i => i.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            var manufacturerVM = new AdministratorManufacturerToCatalogVM();
            manufacturerVM.ManufacturerName = manufacturer.Name;
            manufacturerVM.ManufacturerId = manufacturer.Id;
            manufacturerVM.CatalogIds = manufacturer.Catalogs.Select(i => i.Id).ToList();
            manufacturerVM.AllCatalogs = _context.Catalogs.ToList();

            return View(manufacturerVM);
        }

        // POST: Administrator/Catalogs/AddCatalog/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCatalog(int id, AdministratorManufacturerToCatalogVM manufacturerVM)
        {
            if (id != manufacturerVM.ManufacturerId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var manufacturer = await _context.Manufacturers.Include(i => i.Catalogs).FirstOrDefaultAsync(i => i.Id == id);

                var oldCatalogs = manufacturer.Catalogs.ToList();
                var markedCatalogs = _context.Catalogs.Where(i => manufacturerVM.CatalogIds.Contains(i.Id)).ToList();
                var removedCatalogs = oldCatalogs.Except(markedCatalogs).ToList();
                var addedCatalogs = markedCatalogs.Except(oldCatalogs).ToList();

                manufacturer.Catalogs.AddRange(addedCatalogs);
                foreach (var removedCatalog in removedCatalogs)
                {
                    manufacturer.Catalogs.Remove(removedCatalog);
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Details), new { id = manufacturer.Id });
            }

            return View(manufacturerVM);
        }
    }
}