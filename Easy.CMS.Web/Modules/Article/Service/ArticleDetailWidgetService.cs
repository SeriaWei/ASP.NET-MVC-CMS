using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Web.CMS.Widget;
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
            long.TryParse(httpContext.Request.QueryString["id"], out articleId);
            var articleService = new ArticleService();

            var viewModel = new ArticleDetailViewModel
            {
                Current = articleService.Get(articleId)
            };
            if (viewModel.Current != null)
            {
                return widget.ToWidgetPart(viewModel);
            }
            else
            {
                return widget.ToWidgetPart(null);
            }
        }
    }
}