using Easy.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Layout
{
    public class ViewDataLayoutsAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.Layouts] = ServiceLocator.Current.GetInstance<ILayoutService>(). Get().ToDictionary(m => m.ID, m => m.LayoutName);
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {

        }
    }
}
