/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.Web.Attribute;
using Easy.Web.CMS;
using Easy.Web.CMS.Product.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Product.ActionFilter
{
    public class ViewDataProductCategoryAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.ProductCategory] = new SelectList(ServiceLocator.Current.GetInstance<IProductCategoryService>().Get(), "ID", "Title");
        }
    }
}