using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
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
    public class TeamsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public TeamsController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/Teams
        public async Task<IActionResult> Index()
        {
            var applicationContext = _context.Teams.Include(s => s.Image);
            return View(await applicationContext.ToListAsync());
        }

        // GET: Administrator/Teams/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // GET: Administrator/Teams/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/Teams/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorTeamVM teamVM)
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
                using (var fileStream = new FileStream(Path.Combine(path, teamVM.File.FileName), FileMode.Create))
                {
                    await teamVM.File.CopyToAsync(fileStream);
                }

                Models.File file = new Models.File { Name = teamVM.File.FileName, Path = path, Description = teamVM.FileCaption };
                _context.Files.Add(file);
                _context.SaveChanges();

                teamVM.Team.Image = file;

                _context.Teams.Add(teamVM.Team);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(teamVM);
        }

        // GET: Administrator/Teams/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            var teamVM = new AdministratorTeamVM();
            teamVM.Team = team;
            teamVM.FileCaption = team.Image.Description;

            return View(teamVM);
        }

        // POST: Administrator/Teams/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorTeamVM teamVM)
        {
            if (id != teamVM.Team.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var team = await _context.Teams.Include(i => i.Image).FirstOrDefaultAsync(i => i.Id == id);
                int imageId = team.Image.Id;
                team = teamVM.Team;

                if (teamVM.File is not null)
                {
                    // путь к папке Files
                    string path = Path.Combine(_appEnvironment.WebRootPath, "files");
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    // сохраняем файл в папку Files в каталоге wwwroot
                    using (var fileStream = new FileStream(Path.Combine(path, teamVM.File.FileName), FileMode.Create))
                    {
                        await teamVM.File.CopyToAsync(fileStream);
                    }

                    Models.File file = new Models.File { Name = teamVM.File.FileName, Path = path, Description = teamVM.FileCaption };
                    _context.Files.Add(file);
                    _context.SaveChanges();

                    team.Image = file;
                }
                else
                {
                    Models.File file = _context.Files.FirstOrDefault(i => i.Id == imageId);
                    file.Description = teamVM.FileCaption;

                    _context.SaveChanges();
                }
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(teamVM);
        }

        // GET: Administrator/Teams/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var team = await _context.Teams
                .Include(s => s.Image)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (team == null)
            {
                return NotFound();
            }

            return View(team);
        }

        // POST: Administrator/Teams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeamExists(int id)
        {
            return _context.Teams.Any(e => e.Id == id);
        }
    }
}
