using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorManufacturerToCatalogVM
{
    public int ManufacturerId { get; set; }
    public string ManufacturerName { get; set; }
    public List<int> CatalogIds {get; set; }
    public List<Catalog> AllCatalogs {get; set; }
}