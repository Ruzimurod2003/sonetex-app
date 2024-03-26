using SonetexApp.Models;

namespace SonetexApp.Areas.Main.ViewModels;
public class MainCatalogVM
{
    public List<Catalog> Catalogs { get; set; }
    public List<string> FirstLetters { get; set; }
    public string FirstLetter { get; set; }
}