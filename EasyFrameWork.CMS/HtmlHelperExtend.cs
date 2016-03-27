using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Easy.Extend;
using Easy.Web.CMS.Widget;

namespace Easy.Web.CMS
{
    public static class HtmlHelperExtend
    {
        public static void DisPlayWidget(this HtmlHelper html, WidgetPart widget)
        {
            if (widget.ViewModel != null)
            {
                html.RenderPartial(widget.Widget.PartialView, widget.ViewModel);
            }
            else
            {
                html.WidgetError();
            }
        }

        public static void DesignWidget(this HtmlHelper html, DesignWidgetViewModel viewModel)
        {
            html.RenderPartial("DesignWidget", viewModel);
        }
        public static MvcHtmlString SmartLink(this HtmlHelper html, string link, string text, string cssClass = null)
        {
            if (link.IsNullOrEmpty())
            {
                link = "/";
            }
            bool self = IsOpenSelf(link);
            return MvcHtmlString.Create("<a " + (cssClass.IsNullOrWhiteSpace() ? "" : "class=\"" + cssClass + "\"") + " target=\"" + (self ? "_self" : "_blank") + "\" href=\"" + link + "\">" + text + "</a>");
        }

        public static MvcHtmlString SmartLinkTarget(this HtmlHelper html, string link)
        {
            if (link.IsNullOrEmpty())
            {
                return MvcHtmlString.Create("_self");
            }
            bool self = IsOpenSelf(link);
            return MvcHtmlString.Create(self ? "_self" : "_blank");
        }

        private static bool IsOpenSelf(string link)
        {
            if (HttpContext.Current != null && (link.StartsWith("http://") || link.StartsWith("https://")))
            {
                return new Uri(link).Host.Equals(HttpContext.Current.Request.Url.Host);
            }
            return true;
        }

        public static void WidgetError(this HtmlHelper html)
        {
             html.RenderPartial("Widget.Error");
        }
    }
}
