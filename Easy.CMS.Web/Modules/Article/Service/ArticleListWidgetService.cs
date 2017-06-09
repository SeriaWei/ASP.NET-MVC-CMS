/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */

using System.Linq;
using System.Web;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Data;
using Easy.Web.CMS.Article.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;
using Easy.Web.CMS;

namespace Easy.CMS.Article.Service
{
    public class ArticleListWidgetService : WidgetService<ArticleListWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            var currentWidget = widget as ArticleListWidget;
            var articleTypeService = ServiceLocator.Current.GetInstance<IArticleTypeService>();
            int pageIndex = controllerContext.RouteData.GetPage();
            int category = controllerContext.RouteData.GetCategory();
            var filter = new DataFilter();
            filter.Where("IsPublish", OperatorType.Equal, true);
            filter.OrderBy("CreateDate", OrderType.Descending);
            var articleService = ServiceLocator.Current.GetInstance<IArticleService>();
            var pagin = new Pagination { PageIndex = pageIndex, PageSize = currentWidget.PageSize ?? 20 };
            if (category != 0)
            {
                filter.Where("ArticleTypeID", OperatorType.Equal, category);
            }
            else
            {
                var ids = articleTypeService.Get(new DataFilter().Where("ParentID", OperatorType.Equal, currentWidget.ArticleTypeID)).Select(m => m.ID);
                if (ids.Any())
                {
                    filter.Where("ArticleTypeID", OperatorType.In, ids.Concat(new[] { currentWidget.ArticleTypeID }));
                }
                else
                {
                    filter.Where("ArticleTypeID", OperatorType.Equal, currentWidget.ArticleTypeID);
                }
            }

            var categoryEntity = articleTypeService.Get(category == 0 ? currentWidget.ArticleTypeID : category);
            if (categoryEntity != null)
            {
                var page = controllerContext.HttpContext.GetLayout().Page;
                page.Title = (page.Title ?? "") + " - " + categoryEntity.Title;
            }

            return widget.ToWidgetPart(new ArticleListWidgetViewModel
            {
                Articles = currentWidget.IsPageable ? articleService.Get(filter, pagin) : articleService.Get(filter),
                Widget = currentWidget,
                Pagin = pagin,
                CategoryTitle = categoryEntity == null ? "" : categoryEntity.Title,
                IsPageable = currentWidget.IsPageable
            });
        }
    }
}