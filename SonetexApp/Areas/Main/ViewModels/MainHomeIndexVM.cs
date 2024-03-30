using SonetexApp.Models;

namespace SonetexApp.Areas.Main.ViewModels;
public class MainHomeIndexVM
{
    public List<Catalog> Catalogs { get; set; }
    public List<Manufacturer> Manufacturers { get; set; }
    public List<Partner> Partners { get; set; }
}