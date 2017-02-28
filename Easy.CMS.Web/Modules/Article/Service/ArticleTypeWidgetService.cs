/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS.Article.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;
using Easy.Web.CMS;

namespace Easy.CMS.Article.Service
{
    public class ArticleTypeWidgetService : WidgetService<ArticleTypeWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            int category = controllerContext.RouteData.GetCategory();  

            ArticleTypeWidget currentWidget = widget as ArticleTypeWidget;
            var service = ServiceLocator.Current.GetInstance<IArticleTypeService>();
            var filter = new DataFilter().Where("ParentID", OperatorType.Equal, currentWidget.ArticleTypeID);
            return widget.ToWidgetPart(new ArticleTypeWidgetViewModel
            {
                ArticleTypes = service.Get(filter),
                CurrentType = service.Get(currentWidget.ArticleTypeID),
                TargetPage = currentWidget.TargetPage.IsNullOrEmpty() ? controllerContext.HttpContext.Request.Url.PathAndQuery.ToLower() : currentWidget.TargetPage,
                ArticleTypeID = category
            });
        }
    }
}