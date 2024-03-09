using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using SonetexApp.Areas.Main.ViewModels;
using System.Text;
using System.Text.Encodings.Web;

namespace SonetexApp.Helpers;
public static class PaginationHelper
{
    public static HtmlString CreatePaginationList(
        this IHtmlHelper html,
        PageInfoVM pageInfo,
        Func<int, string> pageUrl)
    {
        StringBuilder result = new StringBuilder();
        TagBuilder ul = new TagBuilder("ul");
        ul.AddCssClass("rbt-pagination");

        if (pageInfo.HasPreviousPage)
        {
            TagBuilder li = new TagBuilder("li");

            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrl(pageInfo.PageNumber - 1));
            a.MergeAttribute("aria-label", "Previous");

            TagBuilder i = new TagBuilder("i");
            i.AddCssClass("feather-chevron-left");
            a.InnerHtml.AppendHtml(i);

            li.InnerHtml.AppendHtml(a);

            ul.InnerHtml.AppendHtml(li);
        }

        for (int i = 1; i <= pageInfo.TotalPages; i++)
        {
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

            ul.InnerHtml.AppendHtml(li);
        }

        if (pageInfo.HasNextPage)
        {
            TagBuilder li = new TagBuilder("li");

            TagBuilder a = new TagBuilder("a");
            a.MergeAttribute("href", pageUrl(pageInfo.PageNumber + 1));
            a.MergeAttribute("aria-label", "Next");

            TagBuilder i = new TagBuilder("i");
            i.AddCssClass("feather-chevron-right");
            a.InnerHtml.AppendHtml(i);

            li.InnerHtml.AppendHtml(a);

            ul.InnerHtml.AppendHtml(li);
        }

        var writer = new System.IO.StringWriter();
        ul.WriteTo(writer, HtmlEncoder.Default);
        return new HtmlString(writer.ToString());
    }
}