using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorTeamVM
{
    public Team Team { get; set; }
    public IFormFile File { get; set; }
    public string FileCaption { get; set; }
}
