using Easy.Web.CMS.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Extend;
using Easy.Web.Attribute;

namespace Easy.Web.CMS.Widget
{
    public class ViewData_ZonesAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            ViewResult result = filterContext.Result as ViewResult;
            if (result != null)
            {
                if (result.Model is WidgetBase)
                {
                    WidgetBase widget = result.Model as WidgetBase;
                    if (!widget.PageID.IsNullOrEmpty())
                    {
                        filterContext.Controller.ViewData[ViewDataKeys.Zones] = new ZoneService().GetZonesByPageId(widget.PageID).ToDictionary(m => m.ID, m => m.ZoneName);
                    }
                    else if (!widget.LayoutID.IsNullOrEmpty())
                    {
                        filterContext.Controller.ViewData[ViewDataKeys.Zones] = new ZoneService().GetZonesByLayoutId(widget.LayoutID).ToDictionary(m => m.ID, m => m.ZoneName);
                    }
                }
            }
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
        }
    }
}
