using Easy.Data;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Web.CMS.Widget;
using Easy.Modules.DataDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;

namespace Easy.CMS.Article.Service
{
    public class ArticleListWidgetService : WidgetService<ArticleListWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var currentWidget = widget as ArticleListWidget;
            var articleTypeService = new ArticleTypeService();
            var categoryEntity = articleTypeService.Get(currentWidget.ArticleTypeID);
            int pageIndex = 0;
            int ac = 0;
            int.TryParse(httpContext.Request.QueryString["ac"], out ac);
            int.TryParse(httpContext.Request.QueryString["p"], out pageIndex);
            var filter = new Data.DataFilter();
            filter.Where("IsPublish", OperatorType.Equal, true);
            filter.OrderBy("PublishDate", OrderType.Descending);
            var articleService = new ArticleService();
            var page = new Data.Pagination { PageIndex = pageIndex, PageSize = currentWidget.PageSize ?? 20 };
            if (ac != 0)
            {
                filter.Where("ArticleTypeID", OperatorType.Equal, ac);
            }
            else
            {
                filter.Where("ArticleTypeID", OperatorType.Equal, currentWidget.ArticleTypeID);
            }
            return widget.ToWidgetPart(new ArticleListWidgetViewModel
            {
                Articles = currentWidget.IsPageable ? articleService.Get(filter, page) : articleService.Get(filter),
                Widget = currentWidget,
                Pagin = page,
                CategoryTitle = categoryEntity == null ? "" : categoryEntity.Title,
                IsPageable = currentWidget.IsPageable
            });
        }
    }
}