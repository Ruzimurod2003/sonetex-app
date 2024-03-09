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
        public IActionResult Index(int page = 1)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            MainManufacturerVM viewModel = new MainManufacturerVM();

            var manufacturers = _manufacturerRepository.GetManufacturers(currentCultureName);

            int pageSize = 6; // количество объектов на страницу
            List<Manufacturer> manufacturersPerPages = manufacturers
                                            .Skip((page - 1) * pageSize)
                                            .Take(pageSize)
                                            .ToList();

            PageInfoVM pageInfo = new PageInfoVM
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = manufacturers.Count
            };
            viewModel.PageInfo = pageInfo;
            viewModel.Manufacturers = manufacturersPerPages;
            viewModel.ManufacturersCount = manufacturers.Count;

            return View(viewModel);
        }
        public IActionResult Details(int id)
        {
            string currentCultureName = HttpContext.Features.Get<IRequestCultureFeature>().RequestCulture.Culture.Name;
            var manufacturer = new Manufacturer();
            var dbManufacturer = _context.Manufacturers.Include(i => i.Catalogs).Include(i => i.Image).FirstOrDefault(i => i.Id == id);
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

            return View(manufacturer);
        }
    }
}
