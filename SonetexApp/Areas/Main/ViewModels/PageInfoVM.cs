namespace SonetexApp.Areas.Main.ViewModels;
public class PageInfoVM
{
    public int PageNumber { get; set; } // номер текущей страницы
    public int PageSize { get; set; } // кол-во объектов на странице
    public int TotalItems { get; set; } // всего объектов
    public bool HasPreviousPage
    {
        get
        {
            return (bool)(PageNumber > 1);
        }
    }
    public bool HasNextPage
    {
        get
        {
            return (bool)(PageNumber < TotalPages);
        }
    }
    public int TotalPages  // всего страниц
    {
        get { return (int)Math.Ceiling((decimal)TotalItems / PageSize); }
    }
}