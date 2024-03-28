using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;

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
        public async Task<IActionResult> Index(string searchString = null, int page = 1)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            AdministratorIndexManufacturerVM viewModel = new AdministratorIndexManufacturerVM();
            var manufacturers = new List<Manufacturer>();
            if (string.IsNullOrEmpty(searchString))
            {
                manufacturers = await _context.Manufacturers.ToListAsync();
            }
            else
            {
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.Name.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.NameRussian.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.NameUzbek.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.NameEnglish.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.Description.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.DescriptionRussian.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.DescriptionUzbek.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.DescriptionEnglish.Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.ImageId.ToString().Contains(searchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.Id.ToString().Contains(searchString)).ToListAsync());
            }
            manufacturers = manufacturers.Distinct().ToList();

            int pageSize = 50; // количество объектов на страницу
            List<Manufacturer> manufacturersPerPages = manufacturers
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = manufacturers.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Manufacturers = manufacturersPerPages;
            viewModel.SearchString = searchString;

            return View(viewModel);
        }
        // POST: Administrator/Manufacturers
        [HttpPost]
        public async Task<IActionResult> Index(AdministratorIndexManufacturerVM viewModel)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var manufacturers = new List<Manufacturer>();

            if (string.IsNullOrEmpty(viewModel.SearchString))
            {
                manufacturers = await _context.Manufacturers.ToListAsync();
            }
            else
            {
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.Name.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.NameRussian.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.NameUzbek.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.NameEnglish.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.Description.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.DescriptionRussian.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.DescriptionUzbek.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.DescriptionEnglish.Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.ImageId.ToString().Contains(viewModel.SearchString)).ToListAsync());
                manufacturers.AddRange(await _context.Manufacturers.Where(i => i.Id.ToString().Contains(viewModel.SearchString)).ToListAsync());
            }

            manufacturers = manufacturers.Distinct().ToList();
            int page = 1;
            int pageSize = 50; // количество объектов на страницу
            List<Manufacturer> manufacturersPerPages = manufacturers
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = manufacturers.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Manufacturers = manufacturersPerPages;

            return View(viewModel);
        }

        // GET: Administrator/Manufacturers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var manufacturer = await _context.Manufacturers
                .Include(i => i.Image)
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

                manufacturerVM.Manufacturer.Image = file;

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

            var manufacturer = await _context.Manufacturers.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
            if (manufacturer == null)
            {
                return NotFound();
            }

            var manufacturerVM = new AdministratorManufacturerVM();
            manufacturerVM.Manufacturer = manufacturer;
            manufacturerVM.FileCaption = manufacturer.Image.Description;

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
                var manufacturer = await _context.Manufacturers.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                int imageId = manufacturer.Image.Id;
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
                    manufacturer.Image = file;
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
                .Include(i => i.Image)
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