using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorManufacturerVM
{
    public Manufacturer Manufacturer { get; set; }
    public IFormFile File { get; set; }
    public string FileCaption { get; set; }
}
