using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorSpecialistVM
{
    public Specialist Specialist { get; set; }
    public IFormFile File { get; set; }
    public string FileCaption { get; set; }
}
