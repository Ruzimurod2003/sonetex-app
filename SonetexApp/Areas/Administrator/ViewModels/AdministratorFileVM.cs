namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorFileVM
{
    public List<Models.File> Files { get; set; }
    public PageInfoVM PageInfo { get; set; }
    public string SearchString { get; set; }
    public int Page { get; set; }
}