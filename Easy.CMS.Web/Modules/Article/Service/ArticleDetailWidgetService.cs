using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Article.Service
{
    public class ArticleDetailWidgetService : WidgetService<ArticleDetailWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            long articleId = 0;
            long.TryParse(httpContext.Request.QueryString["ArticleID"], out articleId);
            var articleService = new ArticleService();

            var viewModel = new ArticleDetailViewModel
            {
                Current = articleService.Get(articleId)
            };

            return widget.ToWidgetPart(viewModel);
        }
    }
}