namespace SonetexApp.Commons;
public static class Functions
{
    public static string CheckCurrentUrlLocatedInThis(string currentUrl, string areaName, string controllerName)
    {
        if (currentUrl.ToLower().Contains($"{areaName}/{controllerName}".ToLower()))
        {
            return "active";
        }
        else
        {
            return string.Empty;
        }
    }
}