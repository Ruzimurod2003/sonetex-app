using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Administrator.ViewModels;
using SonetexApp.Data;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class FilesController : Controller
    {
        private readonly ApplicationContext _context;

        public FilesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Administrator/Files
        public async Task<IActionResult> Index(string searchString = null, int page = 1)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            AdministratorFileVM viewModel = new AdministratorFileVM();
            var files = new List<Models.File>();
            if (string.IsNullOrEmpty(searchString))
            {
                files = await _context.Files.ToListAsync();
            }
            else
            {
                files.AddRange(await _context.Files.Where(i => i.Name.Contains(searchString)).ToListAsync());
                files.AddRange(await _context.Files.Where(i => i.Path.Contains(searchString)).ToListAsync());
                files.AddRange(await _context.Files.Where(i => i.Description.Contains(searchString)).ToListAsync());
                files.AddRange(await _context.Files.Where(i => i.Id.ToString().Contains(searchString)).ToListAsync());
            }
            files = files.Distinct().ToList();

            int pageSize = 50; // количество объектов на страницу
            List<Models.File> filesPerPages = files
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = files.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Files = filesPerPages;
            viewModel.SearchString = searchString;

            return View(viewModel);
        }
        // POST: Administrator/Files
        [HttpPost]
        public async Task<IActionResult> Index(AdministratorFileVM viewModel)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var files = new List<Models.File>();
            if (string.IsNullOrEmpty(viewModel.SearchString))
            {
                files = await _context.Files.ToListAsync();
            }
            else
            {
                files.AddRange(await _context.Files.Where(i => i.Name.Contains(viewModel.SearchString)).ToListAsync());
                files.AddRange(await _context.Files.Where(i => i.Path.Contains(viewModel.SearchString)).ToListAsync());
                files.AddRange(await _context.Files.Where(i => i.Description.Contains(viewModel.SearchString)).ToListAsync());
                files.AddRange(await _context.Files.Where(i => i.Id.ToString().Contains(viewModel.SearchString)).ToListAsync());
            }
            files = files.Distinct().ToList();
            int page = 1;
            int pageSize = 50; // количество объектов на страницу
            List<Models.File> filesPerPages = files
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = files.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Files = filesPerPages;

            return View(viewModel);
        }

        // GET: Administrator/Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @file = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@file == null)
            {
                return NotFound();
            }

            return View(@file);
        }

        // GET: Administrator/Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Files/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Models.File file)
        {
            if (ModelState.IsValid)
            {
                _context.Add(file);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Administrator/Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @file = await _context.Files.FindAsync(id);
            if (@file == null)
            {
                return NotFound();
            }
            return View(@file);
        }

        // POST: Administrator/Files/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Models.File file)
        {
            if (id != file.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(file);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Administrator/Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @file = await _context.Files
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@file == null)
            {
                return NotFound();
            }

            return View(@file);
        }

        // POST: Administrator/Files/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @file = await _context.Files.FindAsync(id);
            if (@file != null)
            {
                _context.Files.Remove(@file);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileExists(int id)
        {
            return _context.Files.Any(e => e.Id == id);
        }
    }
}
