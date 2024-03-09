using SonetexApp.Models;

namespace SonetexApp.Areas.Main.ViewModels;
public class MainCatalogVM
{
    public List<Catalog> Catalogs { get; set; }
    public int CatalogsCount { get; set; }
    public PageInfoVM PageInfo { get; set; }
}