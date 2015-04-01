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

namespace Easy.Web.CMS.Filter
{
    public class EditWidgetAttribute : FilterAttribute, IActionFilter
    {

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ZoneWidgetCollection zones = new ZoneWidgetCollection();

            //Page
            string pageId = filterContext.RequestContext.HttpContext.Request.QueryString["ID"];
            PageService pageService = new PageService();
            PageEntity page = pageService.Get(pageId);
            LayoutEntity layout = null;
            if (page != null)
            {
                LayoutService layoutService = new LayoutService();
                layout = layoutService.Get(page.LayoutId);
                layout.Page = page;
                WidgetService widgetService = new WidgetService();
                IEnumerable<WidgetBase> widgets = widgetService.Get(new DataFilter().Where<WidgetBase>(m => m.PageID, OperatorType.Equal, page.ID));
                Action<WidgetBase> processWidget = m =>
                {
                    IWidgetPartDriver partDriver = Loader.CreateInstance<IWidgetPartDriver>(m.AssemblyName, m.ServiceTypeName);
                    WidgetPart part = partDriver.Display(partDriver.GetWidget(m), filterContext.HttpContext);
                    if (zones.ContainsKey(part.Widget.ZoneID))
                    {
                        zones[part.Widget.ZoneID].Add(part);
                    }
                    else
                    {
                        WidgetCollection partCollection = new WidgetCollection();
                        partCollection.Add(part);
                        zones.Add(part.Widget.ZoneID, partCollection);
                    }
                };
                widgets.Each(processWidget);

                IEnumerable<WidgetBase> Layoutwidgets = widgetService.Get(new Data.DataFilter().Where<WidgetBase>(m => m.LayoutID, OperatorType.Equal, page.LayoutId));

                Layoutwidgets.Each(processWidget);

                layout.ZoneWidgets = zones;
                ViewResult viewResult = (filterContext.Result as ViewResult);
                if (viewResult != null)
                {
                    viewResult.MasterName = "~/Modules/Common/Views/Shared/_DesignPageLayout.cshtml";
                }
                viewResult.ViewData[LayoutEntity.LayoutKey] = layout;
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
