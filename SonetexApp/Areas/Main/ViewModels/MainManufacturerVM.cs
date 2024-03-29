using SonetexApp.Models;

namespace SonetexApp.Areas.Main.ViewModels;
public class MainManufacturerVM
{
    public List<Manufacturer> Manufacturers { get; set; }
    public List<string> FirstLetters { get; set; }
    public string FirstLetter { get; set; }
}