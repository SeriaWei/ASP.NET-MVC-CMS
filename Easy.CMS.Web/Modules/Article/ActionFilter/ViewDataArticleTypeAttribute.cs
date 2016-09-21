using System.Web.Mvc;
using Easy.Web.Attribute;
using Easy.Web.CMS;
using Easy.Web.CMS.Article.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Article.ActionFilter
{
    public class ViewDataArticleTypeAttribute : ViewDataAttribute
    {
        private IArticleTypeService _articleTypeService;

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {

        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _articleTypeService = _articleTypeService ?? ServiceLocator.Current.GetInstance<IArticleTypeService>();
            filterContext.Controller.ViewData[ViewDataKeys.ArticleCategory] = new SelectList(_articleTypeService.Get(), "ID", "Title");
        }
    }
}