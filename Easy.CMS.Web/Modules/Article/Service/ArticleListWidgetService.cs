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
            var categorys = Loader.CreateInstance<IDataDictionaryService>().GetChildren(Constant.DicKeys.ArticleCategory, currentWidget.ArticleCategory);
            int category = 0;
            string categoryStr = httpContext.Request.QueryString["ArticleCategory"];
            int pageIndex = 0;
            int.TryParse(httpContext.Request.QueryString["p"], out pageIndex);

            ArticleListWidgetViewModel viewModel = new ArticleListWidgetViewModel
            {
                Widget = currentWidget,
                ArticleCategory = categorys,
                Pagin = new Data.Pagination { PageIndex = pageIndex }
            };
            var filter = new Data.DataFilter();
            filter.Where("IsPublish=true");
            if (!categoryStr.IsNullOrEmpty())
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
            else if (categorys.Any())
            {
                viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", Constant.OperatorType.In, categorys.ToList(m => m.ID)), viewModel.Pagin);
            }
            return widget.ToWidgetPart(viewModel);
        }
    }
}