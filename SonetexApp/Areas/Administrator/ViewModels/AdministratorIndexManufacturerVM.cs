using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorIndexManufacturerVM
{
    public List<Manufacturer> Manufacturers { get; set; }
    public PageInfoVM PageInfo { get; set; }
    public string SearchString { get; set; }
    public int Page { get; set; }
}
