using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorCatalogToManufacturer
{
    public int CatalogId { get; set; }
    public string CatalogName { get; set; }
    public List<int> ManufaturerIds {get; set; }
    public List<Manufacturer> AllManufaturers {get; set; }
}