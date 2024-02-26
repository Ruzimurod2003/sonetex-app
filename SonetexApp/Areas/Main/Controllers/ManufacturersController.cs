﻿using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SonetexApp.Areas.Main.ViewModels;
using SonetexApp.Data;
using SonetexApp.Models;

namespace SonetexApp.Areas.Main.Controllers
{
    [Area("Main")]
    public class ManufacturersController : Controller
    {
        private readonly ApplicationContext _context;

        public ManufacturersController(ApplicationContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            AdministratorManufacturerVM viewModel = new AdministratorManufacturerVM();
            viewModel.Manufacturers = new List<Manufacturer>();
            var dbManufacturers = _context.Manufacturers
                                    .Include(i => i.File)
                                    .Include(i => i.Catalogs)
                                    .ToList();

            if (currentCultureName == "uz")
            {
                foreach (var dbManufacturer in dbManufacturers)
                {
                    var manufacturer = new Manufacturer();
                    manufacturer.Name = dbManufacturer.NameUzbek;
                    manufacturer.Description = dbManufacturer.DescriptionUzbek;
                    manufacturer.File = dbManufacturer.File;
                    manufacturer.Catalogs = dbManufacturer.Catalogs;
                    manufacturer.Id = dbManufacturer.Id;

                    viewModel.Manufacturers.Add(manufacturer);
                }
            }
            else if (currentCultureName == "ru")
            {
                foreach (var dbManufacturer in dbManufacturers)
                {
                    var manufacturer = new Manufacturer();
                    manufacturer.Name = dbManufacturer.NameRussian;
                    manufacturer.Description = dbManufacturer.DescriptionRussian;
                    manufacturer.File = dbManufacturer.File;
                    manufacturer.Id = dbManufacturer.Id;
                    manufacturer.Catalogs = dbManufacturer.Catalogs;

                    viewModel.Manufacturers.Add(manufacturer);
                }
            }
            else if (currentCultureName == "en")
            {
                foreach (var dbManufacturer in dbManufacturers)
                {
                    var manufacturer = new Manufacturer();
                    manufacturer.Name = dbManufacturer.NameEnglish;
                    manufacturer.Description = dbManufacturer.DescriptionEnglish;
                    manufacturer.File = dbManufacturer.File;
                    manufacturer.Id = dbManufacturer.Id;
                    manufacturer.Catalogs = dbManufacturer.Catalogs;

                    viewModel.Manufacturers.Add(manufacturer);
                }
            }
            else
            {
                foreach (var dbManufacturer in dbManufacturers)
                {
                    var manufacturer = new Manufacturer();
                    manufacturer.Name = dbManufacturer.Name;
                    manufacturer.Description = dbManufacturer.Description;
                    manufacturer.File = dbManufacturer.File;
                    manufacturer.Id = dbManufacturer.Id;
                    manufacturer.Catalogs = dbManufacturer.Catalogs;

                    viewModel.Manufacturers.Add(manufacturer);
                }
            }

            return View(viewModel);
        }
    }
}
