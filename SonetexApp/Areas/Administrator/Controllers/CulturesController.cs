using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.Controllers
{
    [Area("Administrator")]
    [Authorize]
    public class CulturesController : Controller
    {
        private readonly ApplicationContext _context;

        public CulturesController(ApplicationContext context)
        {
            _context = context;
        }

        // GET: Administrator/Cultures
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cultures.ToListAsync());
        }

        // GET: Administrator/Cultures/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = await _context.Cultures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (culture == null)
            {
                return NotFound();
            }

            return View(culture);
        }

        // GET: Administrator/Cultures/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Cultures/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Culture culture)
        {
            if (ModelState.IsValid)
            {
                _context.Add(culture);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(culture);
        }

        // GET: Administrator/Cultures/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = await _context.Cultures.FindAsync(id);
            if (culture == null)
            {
                return NotFound();
            }
            return View(culture);
        }

        // POST: Administrator/Cultures/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Culture culture)
        {
            if (id != culture.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(culture);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CultureExists(culture.Id))
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
            return View(culture);
        }

        // GET: Administrator/Cultures/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var culture = await _context.Cultures
                .FirstOrDefaultAsync(m => m.Id == id);
            if (culture == null)
            {
                return NotFound();
            }

            return View(culture);
        }

        // POST: Administrator/Cultures/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var culture = await _context.Cultures.FindAsync(id);
            if (culture != null)
            {
                _context.Cultures.Remove(culture);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CultureExists(int id)
        {
            return _context.Cultures.Any(e => e.Id == id);
        }
    }
}
