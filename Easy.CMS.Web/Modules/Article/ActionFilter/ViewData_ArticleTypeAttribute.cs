using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.Attribute;
using System.Web.Mvc;
using Easy.CMS.Article.Service;

namespace Easy.CMS.Article.ActionFilter
{
    public class ViewData_ArticleTypeAttribute : ViewDataAttribute
    {
        public override void OnActionExecuted(System.Web.Mvc.ActionExecutedContext filterContext)
        {

        }

        public override void OnActionExecuting(System.Web.Mvc.ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData[ViewDataKeys.ArticleCategory] = new ArticleTypeService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title);
        }
    }
}