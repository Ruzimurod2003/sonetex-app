using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorPartnerVM
{
    public Partner Partner { get; set; }
    public IFormFile File { get; set; }
    public string FileCaption { get; set; }
}
