using SonetexApp.Models;

namespace SonetexApp.Areas.Main.ViewModels;
public class MainProductVM
{
    public List<Product> Products { get; set; }
    public List<Catalog> Catalogs { get; set; }
    public List<Manufacturer> Manufacturers { get; set; }
    public List<Models.Type> Types { get; set; }
    public List<State> States { get; set; }
    public List<int> CatalogIds { get; set; }
    public List<int> ManufacturerIds { get; set; }
    public List<int> StateIds { get; set; }
    public List<int> TypeIds { get; set; }
    public string SearchProduct { get; set; }
    public PageInfoVM PageInfo { get; set; }
    public int ProductsCount { get; set; }
}