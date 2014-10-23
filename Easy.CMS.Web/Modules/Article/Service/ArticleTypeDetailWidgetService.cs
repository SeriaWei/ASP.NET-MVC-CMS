using Easy.Data;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;

namespace Easy.CMS.Article.Service
{
    public class ArticleTypeDetailWidgetService : WidgetService<ArticleTypeDetailWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var currentWidget = widget as ArticleTypeDetailWidget;
            var viewModel = new ArticleTypeDetailWidgetViewModel
            {
                ArticleType = new ArticleTypeService().Get(currentWidget.ArticleType),
                Articles = new ArticleService().Get(new DataFilter().Where("ArticleCategory", OperatorType.Equal, currentWidget.ArticleType).Where("IsPublish=true and status=1"))
            };
            string id = httpContext.Request.QueryString["ID"];
            if (viewModel.Articles.Any())
            {
                viewModel.CurrentArticle = viewModel.Articles.First().ID;
            }
            if (!id.IsNullOrEmpty())
            {
                long articleId = 0;
                if (long.TryParse(id, out articleId))
                {
                    viewModel.CurrentArticle = articleId;
                }

            }
            return currentWidget.ToWidgetPart(viewModel);
        }
    }
}