using SonetexApp.Models;

namespace SonetexApp.Areas.Main.ViewModels;
public class MainManufacturerVM
{
    public List<Manufacturer> Manufacturers { get; set; }
    public int ManufacturersCount { get; set; }
    public PageInfoVM PageInfo { get; set; }
}