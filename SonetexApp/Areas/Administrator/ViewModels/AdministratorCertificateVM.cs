using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorCertificateVM
{
    public Certificate Certificate { get; set; }
    public IFormFile File { get; set; }
    public string FileCaption { get; set; }
}