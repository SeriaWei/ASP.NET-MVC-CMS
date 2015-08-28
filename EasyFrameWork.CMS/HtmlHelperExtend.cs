using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            return MvcHtmlString.Create("<a target=\"" + ((link.StartsWith("http://") || link.StartsWith("https://")) ? "_blank" : "_self") + "\" href=\"" + link + "\">" + text + "</a>");
        }
    }
}
