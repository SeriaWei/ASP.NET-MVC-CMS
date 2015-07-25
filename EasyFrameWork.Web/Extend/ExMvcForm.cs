using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Easy.Web.Extend
{
    public static class ExMvcForm
    {

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper)
        {
            var mvcForm = htmlHelper.BeginForm();
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, object routeValues)
        {
            var mvcForm = htmlHelper.BeginForm(routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues)
        {
            var mvcForm = htmlHelper.BeginForm(routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, method);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, FormMethod method, object htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, routeValues, method);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, routeValues, method);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, object routeValues, FormMethod method, object htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, routeValues, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginTokenForm(this HtmlHelper htmlHelper, string actionName, string controllerName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginForm(actionName, controllerName, routeValues, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, object routeValues)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, RouteValueDictionary routeValues)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, FormMethod method)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, method);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, object routeValues)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, routeValues);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, FormMethod method, object htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, routeValues, method);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, routeValues, method);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, object routeValues, FormMethod method, object htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, routeValues, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static MvcForm BeginRouteTokenForm(this HtmlHelper htmlHelper, string routeName, RouteValueDictionary routeValues, FormMethod method, IDictionary<string, object> htmlAttributes)
        {
            var mvcForm = htmlHelper.BeginRouteForm(routeName, routeValues, method, htmlAttributes);
            htmlHelper.ViewContext.Writer.Write(htmlHelper.AntiForgeryToken().ToHtmlString());
            return mvcForm;
        }

        public static void EndTokenForm(this HtmlHelper htmlHelper)
        {
            htmlHelper.EndForm();
        }
    }
}
