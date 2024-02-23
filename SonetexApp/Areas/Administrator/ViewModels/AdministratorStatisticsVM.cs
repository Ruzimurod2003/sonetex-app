using SonetexApp.Models;

namespace SonetexApp.Areas.Administrator.ViewModels;
public class AdministratorStatisticsVM
{
    public List<Culture> Cultures { get; set; }
    public List<Models.File> Files { get; set; }
    public List<Resource> Resources { get; set; }
    public List<Configuration> Configurations { get; set; }
    public List<Catalog> Catalogs { get; set; }
    public List<Models.Type> Types { get; set; }
    public List<Product> Products { get; set; }
    public List<Certificate> Certificates { get; set; }
    public List<Manufacturer> Manufacturers { get; set; }
    public List<Partner> Partners { get; set; }
    public List<Specialist> Specialists { get; set; }
    public List<State> States { get; set; }
    public List<Team> Teams { get; set; }
}