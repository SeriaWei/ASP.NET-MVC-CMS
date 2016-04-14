using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.Attribute;
using System.Web.Mvc;
using Easy.CMS.Product.Service;
using Easy.Web.CMS;

namespace Easy.CMS.Product.ActionFilter
{
    public class ViewDataProductCategoryAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.ProductCategory] = new SelectList(new ProductCategoryService().Get(), "ID", "Title");
        }
    }
}