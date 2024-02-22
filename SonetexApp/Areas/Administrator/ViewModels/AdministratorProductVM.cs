using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorProductVM
{
    public Product Product { get; set; }
    public IFormFileCollection Files { get; set; }
    public string FileCaption { get; set; }
}