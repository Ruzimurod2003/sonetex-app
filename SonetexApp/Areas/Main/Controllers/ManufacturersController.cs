﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;
using SonetexApp.Repositories;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class ManufacturersController : Controller
    {
        private readonly ApplicationContext _context;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturersController(ApplicationContext context, IManufacturerRepository manufacturerRepository)
        {
            _context = context;
            _manufacturerRepository = manufacturerRepository;
        }
        public IActionResult Index(string firstLetter = null)
        {
            MainManufacturerVM viewModel = new MainManufacturerVM();

            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var manufacturers = _manufacturerRepository.GetManufacturers(currentCultureName);

            viewModel.Manufacturers = (string.IsNullOrEmpty(firstLetter)) ?
                                    manufacturers.OrderBy(i => i.Id).ToList() :
                                        manufacturers.Where(i => i.Name.Substring(0, 1).ToUpper() == firstLetter.ToUpper()).OrderBy(i => i.Id).ToList();
            viewModel.FirstLetters = manufacturers.Select(i => i.Name.Substring(0, 1).ToUpper()).OrderBy(i => i).Distinct().ToList();

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var manufacturer = new Manufacturer();
            var dbManufacturer = _context.Manufacturers
                .Include(i => i.Catalogs)
                .Include(i => i.Image)
                .Include(i => i.MProducts)
                .FirstOrDefault(i => i.Id == id);
            List<Manufacturer> manufacturers = new List<Manufacturer>();
            if (currentCultureName == "uz")
            {
                manufacturer.Name = dbManufacturer.NameUzbek;
                manufacturer.Description = dbManufacturer.DescriptionUzbek;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog() { Id = i.Id, Name = i.NameUzbek }).ToList();
            }
            else if (currentCultureName == "ru")
            {
                manufacturer.Name = dbManufacturer.NameRussian;
                manufacturer.Description = dbManufacturer.DescriptionRussian;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog() { Id = i.Id, Name = i.NameRussian }).ToList();
            }
            else if (currentCultureName == "en")
            {
                manufacturer.Name = dbManufacturer.NameEnglish;
                manufacturer.Description = dbManufacturer.DescriptionEnglish;
                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog() { Id = i.Id, Name = i.NameEnglish }).ToList();
            }
            else
            {
                manufacturer.Name = dbManufacturer.Name;
                manufacturer.Description = dbManufacturer.Description;

                manufacturer.Catalogs = dbManufacturer.Catalogs.Select(i => new Catalog() { Id = i.Id, Name = i.Name }).ToList();
            }
            manufacturer.Id = dbManufacturer.Id;
            manufacturer.Image = dbManufacturer.Image;
            manufacturer.MProducts = dbManufacturer.MProducts;

            return View(manufacturer);
        }
    }
}
