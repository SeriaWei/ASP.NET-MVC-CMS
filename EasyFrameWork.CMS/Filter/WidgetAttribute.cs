using System.Threading.Tasks;
using Easy.Data;
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

namespace Easy.Web.CMS.Filter
{
    public class WidgetAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            var zones = new ZoneWidgetCollection();

            //Page
            string path = filterContext.RequestContext.HttpContext.Request.Path;
            if (path.EndsWith("/") && path.Length > 1)
            {
                path = path.Substring(0, path.Length - 1);
                //filterContext.HttpContext.Response.Redirect(path);
                filterContext.Result = new RedirectResult(path);
                return;
            }
            bool publish = !ReView.Review.Equals(filterContext.RequestContext.HttpContext.Request.QueryString[ReView.QueryKey], StringComparison.CurrentCultureIgnoreCase);
            var page = new PageService().GetByPath(path, publish);
            if (page != null)
            {
                var layoutService = new LayoutService();
                LayoutEntity layout = layoutService.Get(page.LayoutId);
                layout.Page = page;

                Action<WidgetBase> processWidget = m =>
                {
                    var partDriver = Loader.CreateInstance<IWidgetPartDriver>(m.AssemblyName, m.ServiceTypeName);
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(m), filterContext.HttpContext);
                    if (zones.ContainsKey(part.Widget.ZoneID))
                    {
                        zones[part.Widget.ZoneID].Add(part);
                    }
                    else
                    {
                        var partCollection = new WidgetCollection { part };
                        zones.Add(part.Widget.ZoneID, partCollection);
                    }
                };
                var layoutWidgetsTask = Task.Factory.StartNew(layoutId =>
                {
                    var widgetServiceIn = new WidgetService();
                    IEnumerable<WidgetBase> layoutwidgets = widgetServiceIn.Get(new DataFilter().Where("LayoutID", OperatorType.Equal, layoutId));
                    layoutwidgets.Each(processWidget);
                }, page.LayoutId);


                var widgetService = new WidgetService();
                IEnumerable<WidgetBase> widgets = widgetService.Get(new DataFilter().Where("PageID", OperatorType.Equal, page.ID));
                int middle = widgets.Count() / 2;
                var pageWidgetsTask = Task.Factory.StartNew(() =>widgets.Skip(middle).Each(processWidget));
                widgets.Take(middle).Each(processWidget);
                Task.WaitAll(new[] { pageWidgetsTask, layoutWidgetsTask });

                layout.ZoneWidgets = zones;
                var viewResult = (filterContext.Result as ViewResult);
                if (viewResult != null)
                {
                    //viewResult.MasterName = "~/Modules/Easy.Web.CMS.Page/Views/Shared/_Layout.cshtml";
                    viewResult.ViewData[LayoutEntity.LayoutKey] = layout;
                }
            }
            else
            {
                filterContext.Result = new HttpStatusCodeResult(404);
            }
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }

}
