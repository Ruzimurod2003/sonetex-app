namespace SonetexApp.Models;

public class Culture
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Resource> Resources { get; set; }
    public Culture()
    {
        Resources = new List<Resource>();
    }
}