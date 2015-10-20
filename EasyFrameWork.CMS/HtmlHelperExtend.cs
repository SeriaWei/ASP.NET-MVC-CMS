using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.CMS
{
    public static class HtmlHelperExtend
    {
        public static MvcHtmlString SmartLink(this HtmlHelper html, string link, string text)
        {
            if (link.IsNullOrEmpty())
            {
                link = "/";
            }
            bool self = !link.StartsWith("http://") && !link.StartsWith("https://");
            return MvcHtmlString.Create("<a target=\"" + (self ? "_self" : "_blank") + "\" href=\"" + link + "\">" + text + "</a>");
        }

        public static MvcHtmlString SmartLinkTarget(this HtmlHelper html, string link)
        {
            if (link.IsNullOrEmpty())
            {
                return MvcHtmlString.Create("_self");
            }
            bool self = !link.StartsWith("http://") && !link.StartsWith("https://");
            return MvcHtmlString.Create(self ? "_self" : "_blank");
        }
    }
}
