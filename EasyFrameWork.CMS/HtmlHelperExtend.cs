using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS.Widget;

namespace Easy.Web.CMS
{
    public static class HtmlHelperExtend
    {
        public static MvcHtmlString DisPlayWidget(this HtmlHelper html, WidgetPart widget)
        {
            if (widget.ViewModel != null)
            {
                return html.Partial(widget.Widget.PartialView, widget.ViewModel);
            }
            else
            {
                return html.WidgetError();
            }
        }

        public static MvcHtmlString DesignWidget(this HtmlHelper html, DesignWidgetViewModel viewModel)
        {
            return html.Partial("DesignWidget", viewModel);
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
               return link.ToLower().Contains(HttpContext.Current.Request.Url.Host.ToLower());
            }
            return true;
        }

        public static MvcHtmlString WidgetError(this HtmlHelper html)
        {
            return html.Partial("Widget.Error");
        }

        public static void Pagin(this HtmlHelper html, Pagination pagin)
        {
            html.RenderPartial("Partial_Pagination", pagin);
        }
    }
}
