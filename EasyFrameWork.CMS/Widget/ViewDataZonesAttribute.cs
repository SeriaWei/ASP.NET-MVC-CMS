using System.Web.Mvc;
using Easy.Extend;
using Easy.Web.Attribute;
using Easy.Web.CMS.Zone;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Widget
{
    public class ViewDataZonesAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult result = filterContext.Result as ViewResult;
            if (result != null)
            {
                if (result.Model is WidgetBase)
                {
                    WidgetBase widget = result.Model as WidgetBase;
                    var zoneService = ServiceLocator.Current.GetInstance<IZoneService>();
                    if (!widget.PageID.IsNullOrEmpty())
                    {
                        filterContext.Controller.ViewData[ViewDataKeys.Zones] = new SelectList(zoneService.GetZonesByPageId(widget.PageID), "ID", "ZoneName");
                    }
                    else if (!widget.LayoutID.IsNullOrEmpty())
                    {
                        filterContext.Controller.ViewData[ViewDataKeys.Zones] = new SelectList(zoneService.GetZonesByLayoutId(widget.LayoutID), "ID", "ZoneName");
                    }
                }
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
        }
    }
}
