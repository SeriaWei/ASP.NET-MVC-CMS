using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.CMS.Widget;
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
            ArticleListWidget currentWidget = widget as ArticleListWidget;
            var articleTypeService = new ArticleTypeService();

            var categorys = articleTypeService.GetChildren(currentWidget.ArticleCategory);
            int category = 0;
            string categoryStr = httpContext.Request.QueryString["ArticleCategory"];
            int pageIndex = 0;
            int.TryParse(httpContext.Request.QueryString["p"], out pageIndex);
            var categoryEntity = articleTypeService.Get(currentWidget.ArticleCategory);
            ArticleListWidgetViewModel viewModel = new ArticleListWidgetViewModel
            {
                Widget = currentWidget,
                ArticleCategory = categorys,
                Pagin = new Data.Pagination { PageIndex = pageIndex },
                CategoryTitle = categoryEntity == null ? "" : categoryEntity.Title
            };
            var filter = new Data.DataFilter();
            filter.Where("IsPublish=true");
            filter.OrderBy("PublishDate", Constant.OrderType.Descending);
            if (!categoryStr.IsNullOrEmpty() && categorys != null)
            {
                if (int.TryParse(categoryStr, out category))
                {
                    viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", Constant.OperatorType.Equal, category), viewModel.Pagin);
                }
                else if (categorys.Any())
                {
                    viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", Constant.OperatorType.In, categorys.ToList(m => m.ID)), viewModel.Pagin);
                }
            }
            else if (categorys != null && categorys.Any())
            {
                viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", Constant.OperatorType.In, categorys.ToList(m => m.ID)), viewModel.Pagin);
            }
            return widget.ToWidgetPart(viewModel);
        }
    }
}