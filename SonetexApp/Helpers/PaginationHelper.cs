using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Text.Encodings.Web;

namespace SonetexApp.Helpers;
public static class PaginationHelper
{
    public static HtmlString CreatePaginationListForMain(
        this IHtmlHelper html,
        Areas.Main.ViewModels.PageInfoVM pageInfo,
        Func<int, string> pageUrl)
    {
        StringBuilder result = new StringBuilder();
        TagBuilder ul = new TagBuilder("ul");
        ul.AddCssClass("rbt-pagination");

        if (pageInfo.HasPreviousPage)
        {
            AddedFirstPaginationElements(ul, pageInfo.PageNumber, pageUrl);
        }

        if (pageInfo.PageNumber <= 3)
        {
            AddedPaginationElements(1, 3, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.TotalPages - 2, pageInfo.TotalPages, ul, pageInfo.PageNumber, pageUrl);
        }
        else if (pageInfo.PageNumber == 4)
        {
            AddedPaginationElements(1, 3, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(4, 6, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.TotalPages - 2, pageInfo.TotalPages, ul, pageInfo.PageNumber, pageUrl);
        }
        else if (pageInfo.PageNumber >= 5 && pageInfo.PageNumber <= pageInfo.TotalPages - 4)
        {
            AddedPaginationElements(1, 3, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.PageNumber - 1, pageInfo.PageNumber + 1, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.TotalPages - 2, pageInfo.TotalPages, ul, pageInfo.PageNumber, pageUrl);
        }
        else if (pageInfo.PageNumber == pageInfo.TotalPages - 3)
        {
            AddedPaginationElements(1, 3, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.TotalPages - 5, pageInfo.TotalPages - 3, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.TotalPages - 2, pageInfo.TotalPages, ul, pageInfo.PageNumber, pageUrl);
        }
        else if (pageInfo.PageNumber >= pageInfo.TotalPages - 2)
        {
            AddedPaginationElements(1, 3, ul, pageInfo.PageNumber, pageUrl);

            AddedPointPaginationElements(ul, pageUrl);

            AddedPaginationElements(pageInfo.TotalPages - 2, pageInfo.TotalPages, ul, pageInfo.PageNumber, pageUrl);
        }

        if (pageInfo.HasNextPage)
        {
            AddedLastPaginationElements(ul, pageInfo.PageNumber, pageUrl);
        }

        var writer = new System.IO.StringWriter();
        ul.WriteTo(writer, HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
    }
    private static void AddedPaginationElements(int from, int to, TagBuilder ul, int pageNumber, Func<int, string> pageUrl)
    {
        for (int i = from; i <= to; i++)
        {
            TagBuilder li = new TagBuilder("li");
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageNumber)
            {
                li.AddCssClass("active");
            }

            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrl(i));
            a.InnerHtml.Append(i.ToString());

            li.InnerHtml.AppendHtml(a);

            ul.InnerHtml.AppendHtml(li);
        }
    }
    private static void AddedLastPaginationElements(TagBuilder ul, int pageNumber, Func<int, string> pageUrl)
    {
        TagBuilder li = new TagBuilder("li");

        TagBuilder a = new TagBuilder("a");
        a.MergeAttribute("href", pageUrl(pageNumber + 1));
        a.MergeAttribute("aria-label", "Next");

        TagBuilder i = new TagBuilder("i");
        i.AddCssClass("feather-chevron-right");
        a.InnerHtml.AppendHtml(i);

        li.InnerHtml.AppendHtml(a);

        ul.InnerHtml.AppendHtml(li);
    }
    private static void AddedFirstPaginationElements(TagBuilder ul, int pageNumber, Func<int, string> pageUrl)
    {
        TagBuilder li = new TagBuilder("li");

        TagBuilder a = new TagBuilder("a");
        a.MergeAttribute("href", pageUrl(pageNumber - 1));
        a.MergeAttribute("aria-label", "Previous");

        TagBuilder i = new TagBuilder("i");
        i.AddCssClass("feather-chevron-left");
        a.InnerHtml.AppendHtml(i);

        li.InnerHtml.AppendHtml(a);

        ul.InnerHtml.AppendHtml(li);
    }
    private static void AddedPointPaginationElements(TagBuilder ul, Func<int, string> pageUrl)
    {
        TagBuilder liPoint = new TagBuilder("li");

        liPoint.InnerHtml.AppendHtml("...");

        ul.InnerHtml.AppendHtml(liPoint);
    }

    public static HtmlString CreatePaginationListForAdministrator(
        this IHtmlHelper html,
        Areas.Administrator.ViewModels.PageInfoVM pageInfo,
        Func<int, string> pageUrl)
    {
        var writer = new System.IO.StringWriter();

        {
            TagBuilder liPoint = new TagBuilder("li");
            TagBuilder aPoint = new TagBuilder("a");
            aPoint.InnerHtml.Append("***");
            liPoint.InnerHtml.AppendHtml(aPoint);
            liPoint.WriteTo(writer, HtmlEncoder.Default);
        }

        for (int i = pageInfo.PageNumber - 1; i <= pageInfo.PageNumber + 1; i++)
        {
            if (i <= 0 || i > pageInfo.TotalPages)
            {
                continue;
            }
            TagBuilder li = new TagBuilder("li");
            // если текущая страница, то выделяем ее,
            // например, добавляя класс
            if (i == pageInfo.PageNumber)
            {
                li.AddCssClass("active");
            }
            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrl(i));
            a.InnerHtml.Append(i.ToString());
            li.InnerHtml.AppendHtml(a);
            li.WriteTo(writer, HtmlEncoder.Default);
        }

        {
            TagBuilder liPoint = new TagBuilder("li");
            TagBuilder aPoint = new TagBuilder("a");
            aPoint.InnerHtml.Append("***");
            liPoint.InnerHtml.AppendHtml(aPoint);
            liPoint.WriteTo(writer, HtmlEncoder.Default);
        }

        return new HtmlString(writer.ToString());
    }
}