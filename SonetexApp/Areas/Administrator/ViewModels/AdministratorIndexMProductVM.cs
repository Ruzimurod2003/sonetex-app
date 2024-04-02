using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorIndexMProductVM
{
    public List<MProduct> MProducts { get; set; }
    public PageInfoVM PageInfo { get; set; }
    public string SearchString { get; set; }
    public int Page { get; set; }
}
