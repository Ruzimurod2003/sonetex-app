using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    public class ProductsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public ProductsController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Products
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.Type)
                .Include(p => p.State);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Administrator/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.Type)
                .Include(i => i.State)
                .Include(i => i.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Administrator/Products/Create
        public IActionResult Create()
        {
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name");
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name");
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name");
            return View();
        }

        // POST: Administrator/Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorProductVM productVM)
        {
            if (ModelState.IsValid)
            {
                List<Models.File> files = new List<Models.File>();
                foreach (var image in productVM.Files)
                {
                    // путь к папке Files
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create))
                    {
                        await image.CopyToAsync(fileStream);
                    }

                    Models.File file = new Models.File { Name = image.FileName, Path = path, Description = productVM.FileCaption };
                    _context.Files.Add(file);
                    _context.SaveChanges();

                    files.Add(file);
                }

                var product = productVM.Product;
                product.Images = files;

                _context.Products.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name", productVM.Product.CatalogId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", productVM.Product.TypeId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", productVM.Product.StateId);

            return View(productVM);
        }

        // GET: Administrator/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name", product.CatalogId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", product.TypeId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", product.TypeId);

            var productVM = new AdministratorProductVM();
            productVM.Product = product;
            productVM.FileCaption = product.Images.FirstOrDefault().Description;

            return View(productVM);
        }

        // POST: Administrator/Products/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorProductVM productVM)
        {
            if (id != productVM.Product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var product = await _context.Products.Include(i => i.Images).FirstOrDefaultAsync(i => i.Id == id);

                List<Models.File> images = new List<Models.File>();

                if (productVM.Files is not null && productVM.Files.Any())
                {
                    foreach (var image in productVM.Files)
                    {
                        // путь к папке Files
                        string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                        if (!Directory.Exists(path))
                        {
                            Directory.CreateDirectory(path);
                        }

                        // сохраняем файл в папку Files в каталоге wwwroot
                        using (var fileStream = new FileStream(Path.Combine(path, image.FileName), FileMode.Create))
                        {
                            await image.CopyToAsync(fileStream);
                        }

                        Models.File file = new Models.File { Name = image.FileName, Path = path, Description = productVM.FileCaption };
                        _context.Files.Add(file);
                        _context.SaveChanges();

                        images.Add(file);
                    }
                    product.Images = images;
                }

                product.Name = productVM.Product.Name;
                product.NameUzbek = productVM.Product.NameUzbek;
                product.NameEnglish = productVM.Product.NameEnglish;
                product.NameRussian = productVM.Product.NameRussian;
                product.Description = productVM.Product.Description;
                product.Availability = productVM.Product.Availability;
                product.VendorCode = productVM.Product.VendorCode;
                product.Guarantee = productVM.Product.Guarantee;
                product.CatalogId = productVM.Product.CatalogId;
                product.Address = productVM.Product.Address;
                product.TypeId = productVM.Product.TypeId;
                product.StateId = productVM.Product.StateId;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["CatalogId"] = new SelectList(_context.Catalogs, "Id", "Name", productVM.Product.CatalogId);
            ViewData["TypeId"] = new SelectList(_context.Types, "Id", "Name", productVM.Product.TypeId);
            ViewData["StateId"] = new SelectList(_context.States, "Id", "Name", productVM.Product.StateId);

            return View(productVM);
        }

        // GET: Administrator/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.State)
                .Include(p => p.Type)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Administrator/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Products
                .Include(p => p.Catalog)
                .Include(p => p.State)
                .Include(p => p.Type)
                .Include(p => p.Images)
                .FirstOrDefaultAsync(i => i.Id == id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}
