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

            var categorys = articleTypeService.GetChildren(currentWidget.ArticleCategory);
            int category = 0;
            string categoryStr = httpContext.Request.QueryString["ArticleCategory"];
            int pageIndex = 0;
            int.TryParse(httpContext.Request.QueryString["p"], out pageIndex);
            var categoryEntity = articleTypeService.Get(currentWidget.ArticleCategory);
            var viewModel = new ArticleListWidgetViewModel
            {
                Widget = currentWidget,
                ArticleCategory = categorys,
                Pagin = new Data.Pagination { PageIndex = pageIndex },
                CategoryTitle = categoryEntity == null ? "" : categoryEntity.Title
            };
            var filter = new Data.DataFilter();
            filter.Where("IsPublish=true");
            filter.OrderBy("PublishDate", OrderType.Descending);
            if (!categoryStr.IsNullOrEmpty() && categorys != null)
            {
                if (int.TryParse(categoryStr, out category))
                {
                    viewModel.CurrentCategory = category;
                    viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", OperatorType.Equal, category), viewModel.Pagin);
                }
                else if (categorys.Any())
                {
                    viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", OperatorType.In, categorys.ToList(m => m.ID)), viewModel.Pagin);
                }
            }
            else if (categorys != null && categorys.Any())
            {
                viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", OperatorType.In, categorys.ToList(m => m.ID)), viewModel.Pagin);
            }
            return widget.ToWidgetPart(viewModel);
        }
    }
}