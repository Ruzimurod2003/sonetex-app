using FanurApp.Localizers;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using SonetexApp.Data;
using SonetexApp.Localizers;
using SonetexApp.Repositories;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationContext>(option =>
    option.UseSqlite(builder.Configuration.GetConnectionString("SonetexSqliteConnection")));

builder.Services.AddTransient<IStringLocalizer, EFStringLocalizer>();

builder.Services.AddTransient<IConfigurationRepository, ConfigurationRepository>();

builder.Services.AddTransient<ICatalogRepository, CatalogRepository>();

builder.Services.AddTransient<IManufacturerRepository, ManufacturerRepository>();

builder.Services.AddTransient<IStateRepository, StateRepository>();

builder.Services.AddTransient<ITypeRepository, TypeRepository>();

builder.Services.AddTransient<IProductRepository, ProductRepository>();

builder.Services.AddSingleton<IStringLocalizerFactory>(new EFStringLocalizerFactory(builder.Configuration.GetConnectionString("SonetexSqliteConnection")));

builder.Services.AddControllersWithViews().AddDataAnnotationsLocalization(options =>
{
    options.DataAnnotationLocalizerProvider = (type, factory) =>
    factory.Create(null);
})
.AddViewLocalization();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/Main/Home/Login");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment() && !app.Environment.IsProduction())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

app.UseHttpsRedirection();

var supportedCultures = new[]
{
    new CultureInfo("en"),
    new CultureInfo("ru"),
    new CultureInfo("uz")
};
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("ru"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures
});

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// маршрут для области Administrator
app.MapAreaControllerRoute(
    name: "main_area",
    areaName: "main",
    pattern: "main/{controller=Home}/{action=Index}/{id?}");

// маршрут для области Administrator
app.MapAreaControllerRoute(
    name: "administrator_area",
    areaName: "administrator",
    pattern: "administrator/{controller=Home}/{action=Index}/{id?}");

// добавляем поддержку для контроллеров, которые располагаются вне области
app.MapControllerRoute(
    name: "default",
    pattern: "{area=Main}/{controller=Home}/{action=Index}/{id?}");

app.Run();
