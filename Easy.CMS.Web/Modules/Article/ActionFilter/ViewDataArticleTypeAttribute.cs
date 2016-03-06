using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.Attribute;
using System.Web.Mvc;
using Easy.CMS.Article.Service;
using Easy.Web.CMS;

namespace Easy.CMS.Article.ActionFilter
{
    public class ViewDataArticleTypeAttribute : ViewDataAttribute
    {
        private ArticleTypeService _articleTypeService;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _articleTypeService = _articleTypeService ?? new ArticleTypeService();
            filterContext.Controller.ViewData[ViewDataKeys.ArticleCategory] = new SelectList(_articleTypeService.Get(), "ID", "Title");
        }
    }
}