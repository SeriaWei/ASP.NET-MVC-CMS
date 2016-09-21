using System.Web.Mvc;
using Easy.Web.Attribute;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Layout
{
    public class ViewDataLayoutsAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.Layouts] = new SelectList(ServiceLocator.Current.GetInstance<ILayoutService>().Get(), "ID", "LayoutName");
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }
    }
}
