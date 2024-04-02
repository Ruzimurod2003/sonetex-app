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
    public class MProductsController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IWebHostEnvironment _appEnvironment;

        public MProductsController(ApplicationContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: Administrator/MProducts
        public async Task<IActionResult> Index(string searchString = null, int page = 1)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            AdministratorIndexMProductVM viewModel = new AdministratorIndexMProductVM();
            var mProducts = new List<MProduct>();
            if (string.IsNullOrEmpty(searchString))
            {
                mProducts = await _context.MProducts.ToListAsync();
            }
            else
            {
                mProducts.AddRange(await _context.MProducts.Where(i => i.Name.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.NameRussian.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.NameUzbek.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.NameEnglish.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.Description.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.DescriptionRussian.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.DescriptionUzbek.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.DescriptionEnglish.ToUpper().Contains(searchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.Id.ToString().ToUpper().Contains(searchString.ToUpper())).ToListAsync());
            }
            mProducts = mProducts.Distinct().ToList();

            int pageSize = 50; // количество объектов на страницу
            List<MProduct> mProductsPerPages = mProducts
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = mProducts.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.MProducts = mProductsPerPages;
            viewModel.SearchString = searchString;

            return View(viewModel);
        }
        // POST: Administrator/MProducts
        [HttpPost]
        public async Task<IActionResult> Index(AdministratorIndexMProductVM viewModel)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var mProducts = new List<MProduct>();

            if (string.IsNullOrEmpty(viewModel.SearchString))
            {
                mProducts = await _context.MProducts.ToListAsync();
            }
            else
            {
                mProducts.AddRange(await _context.MProducts.Where(i => i.Name.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.NameRussian.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.NameUzbek.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.NameEnglish.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.Description.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.DescriptionRussian.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.DescriptionUzbek.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.DescriptionEnglish.ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
                mProducts.AddRange(await _context.MProducts.Where(i => i.Id.ToString().ToUpper().Contains(viewModel.SearchString.ToUpper())).ToListAsync());
            }

            mProducts = mProducts.Distinct().ToList();
            int page = 1;
            int pageSize = 50; // количество объектов на страницу
            List<MProduct> MProductsPerPage = mProducts
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = mProducts.Count,
                PreviousPageNumber = page - 1,
                NextPageNumber = page + 1,
            };
            viewModel.PageInfo = pageInfo;
            viewModel.MProducts = MProductsPerPage;

            return View(viewModel);
        }

        // GET: Administrator/MProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProduct = await _context.MProducts
                .Include(i => i.Manufacturer)
                .ThenInclude(j => j.MProducts)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mProduct == null)
            {
                return NotFound();
            }

            return View(mProduct);
        }

        // GET: Administrator/MProducts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Administrator/MProducts/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AdministratorMProductVM mProductVM)
        {
            if (ModelState.IsValid)
            {
                _context.MProducts.Add(mProductVM.MProduct);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(mProductVM);
        }

        // GET: Administrator/MProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProduct = await _context.MProducts.FirstOrDefaultAsync(i => i.Id == id);
            if (mProduct == null)
            {
                return NotFound();
            }

            var mProductVM = new AdministratorMProductVM();
            mProductVM.MProduct = mProduct;

            return View(mProductVM);
        }

        // POST: Administrator/MProducts/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, AdministratorMProductVM mProductVM)
        {
            if (id != mProductVM.MProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var mProduct = await _context.MProducts.FirstOrDefaultAsync(i => i.Id == id);
                mProduct.Name = mProductVM.MProduct.Name;
                mProduct.NameRussian = mProductVM.MProduct.NameRussian;
                mProduct.NameEnglish = mProductVM.MProduct.NameEnglish;
                mProduct.NameUzbek = mProductVM.MProduct.NameUzbek;
                mProduct.Description = mProductVM.MProduct.Description;
                mProduct.DescriptionUzbek = mProductVM.MProduct.DescriptionUzbek;
                mProduct.DescriptionRussian = mProductVM.MProduct.DescriptionRussian;
                mProduct.DescriptionEnglish = mProductVM.MProduct.DescriptionEnglish;

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            return View(mProductVM);
        }

        // GET: Administrator/MProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mProduct = await _context.MProducts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mProduct == null)
            {
                return NotFound();
            }

            return View(mProduct);
        }

        // POST: Administrator/MProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mProduct = await _context.MProducts.FindAsync(id);
            if (mProduct != null)
            {
                _context.MProducts.Remove(mProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MProductExists(int id)
        {
            return _context.MProducts.Any(e => e.Id == id);
        }
    }
}
