using Blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Blog.Helpers
{
    public static class HtmlExtension
    {
        public static MvcHtmlString PageNavigation(this HtmlHelper html, PageInfo pages, Func<int, string> url)
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int iPage = 1; iPage <= pages.NumberOfPages; iPage++)
            {
                TagBuilder tag = new TagBuilder("a");
                tag.MergeAttribute("href", url(iPage));
                tag.InnerHtml = iPage.ToString();
                
                if(iPage == pages.CurrentPage)
                    tag.AddCssClass("page-current");

                tag.AddCssClass("page-block");
                stringBuilder.Append(tag);
            }
            return MvcHtmlString.Create(stringBuilder.ToString());
        }
    }
}