using Easy.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

namespace Easy.Web.CMS.Layout
{
    public class ViewData_LayoutsAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.Layouts] = new LayoutService().Get().ToDictionary(m => m.ID, m => m.LayoutName);
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {

        }
    }
}
