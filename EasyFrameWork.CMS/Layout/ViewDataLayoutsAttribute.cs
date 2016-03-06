using Easy.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Layout
{
    public class ViewDataLayoutsAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.Layouts] = new SelectList(ServiceLocator.Current.GetInstance<ILayoutService>().Get(), "ID", "LayoutName");
        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {

        }
    }
}
