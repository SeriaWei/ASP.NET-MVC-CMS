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
using System.Web;
using Easy.Cache;
using Easy.Web.CMS.Theme;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Filter
{
    public class WidgetAttribute : FilterAttribute, IActionFilter
    {
        public IPageService PageService
        {
            get { return ServiceLocator.Current.GetInstance<IPageService>(); }
        }
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

            return PageService.GetByPath(path, isPreView);
        }
        public virtual string GetLayout()
        {
            return "~/Modules/Common/Views/Shared/_Layout.cshtml";
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            //Page
            var page = GetPage(filterContext);
            if (page != null)
            {
                LayoutEntity layout = ServiceLocator.Current.GetInstance<ILayoutService>().Get(page.LayoutId);
                layout.Page = page;
                if (filterContext.RequestContext.HttpContext.Request.IsAuthenticated && page.IsPublishedPage)
                {
                    layout.PreViewPage = PageService.GetByPath(page.Url, true);
                }
                layout.CurrentTheme = ServiceLocator.Current.GetInstance<IThemeService>().GetCurrentTheme();
                layout.ZoneWidgets = new ZoneWidgetCollection();
                filterContext.HttpContext.TrySetLayout(layout);
                var widgetService = ServiceLocator.Current.GetInstance<IWidgetService>();
                widgetService.GetAllByPage(page).Each(widget =>
                {
                    IWidgetPartDriver partDriver = widget.CreateServiceInstance();
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(widget), filterContext.HttpContext);
                    lock (layout.ZoneWidgets)
                    {
                        if (layout.ZoneWidgets.ContainsKey(part.Widget.ZoneID))
                        {
                            layout.ZoneWidgets[part.Widget.ZoneID].TryAdd(part);
                        }
                        else
                        {
                            layout.ZoneWidgets.Add(part.Widget.ZoneID, new WidgetCollection { part });
                        }
                    }
                });
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
            var applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>() as CMSApplicationContext;
            if (applicationContext != null && HttpContext.Current != null)
            {
                applicationContext.RequestUrl = HttpContext.Current.Request.Url;
            }
        }
    }

}
