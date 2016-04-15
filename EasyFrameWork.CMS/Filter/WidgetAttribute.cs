using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Layout;
using Easy.Constant;
using Easy.Extend;
using System.Net;
using Easy.Cache;
using Easy.Web.CMS.Theme;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Filter
{
    public class WidgetAttribute : FilterAttribute, IActionFilter
    {
        public ZoneWidgetCollection Zones { get; set; }

        public virtual PageEntity GetPage(ActionExecutedContext filterContext)
        {
            string path = filterContext.RequestContext.HttpContext.Request.Path;
            if (path.EndsWith("/") && path.Length > 1)
            {
                path = path.Substring(0, path.Length - 1);
                filterContext.Result = new RedirectResult(path);
                return null;
            }
            bool isPreView = false;
            if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated)
            {
                isPreView = ReView.Review.Equals(
                    filterContext.RequestContext.HttpContext.Request.QueryString[ReView.QueryKey],
                    StringComparison.CurrentCultureIgnoreCase);
            }

            return ServiceLocator.Current.GetInstance<IPageService>().GetByPath(path, isPreView);
        }

        public virtual string GetLayout()
        {
            return "~/Modules/Common/Views/Shared/_Layout.cshtml";
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            Zones = new ZoneWidgetCollection();
            //Page
            var page = GetPage(filterContext);
            if (page != null)
            {
                var cache = new StaticCache();
                var layoutService = ServiceLocator.Current.GetInstance<ILayoutService>();
                LayoutEntity layout = layoutService.Get(page.LayoutId);
                layout.Page = page;
                layout.CurrentTheme = ServiceLocator.Current.GetInstance<IThemeService>().GetCurrentTheme();
                filterContext.HttpContext.TrySetLayout(layout);
                Action<WidgetBase> processWidget = m =>
                {
                    IWidgetPartDriver partDriver = cache.Get("IWidgetPartDriver_" + m.AssemblyName + m.ServiceTypeName, source =>
                         Activator.CreateInstance(m.AssemblyName, m.ServiceTypeName).Unwrap() as IWidgetPartDriver
                    );
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(m), filterContext.HttpContext);
                    lock (Zones)
                    {
                        if (Zones.ContainsKey(part.Widget.ZoneID))
                        {
                            Zones[part.Widget.ZoneID].TryAdd(part);
                        }
                        else
                        {
                            var partCollection = new WidgetCollection { part };
                            Zones.Add(part.Widget.ZoneID, partCollection);
                        }
                    }
                };
                var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
                IEnumerable<WidgetBase> widgets = widgetService.GetAllByPage(page);
                widgets.AsParallel().ForAll(processWidget);
                layout.ZoneWidgets = Zones;
                var viewResult = (filterContext.Result as ViewResult);
                if (viewResult != null)
                {
                    viewResult.MasterName = GetLayout();
                    filterContext.Controller.ViewData.Model = layout;
                }
            }
            else
            {
                filterContext.Result = new RedirectResult("~/error/notfond");
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }

}
