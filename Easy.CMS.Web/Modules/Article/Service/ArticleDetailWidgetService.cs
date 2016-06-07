using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.CMS.Article.Models;
using Easy.Web.CMS.Article.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Article.Service
{
    public class ArticleDetailWidgetService : WidgetService<ArticleDetailWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            long articleId = 0;
            long.TryParse(httpContext.Request.QueryString["id"], out articleId);


            var viewModel = new ArticleDetailViewModel
            {
                Current = ServiceLocator.Current.GetInstance<IArticleService>().Get(articleId)
            };
            if (viewModel.Current != null)
            {
                var layout = httpContext.GetLayout();
                layout.Page.MetaKeyWorlds = viewModel.Current.MetaKeyWords;
                layout.Page.MetaDescription = viewModel.Current.MetaDescription;
                layout.Page.Title = viewModel.Current.Title;

            }
            else
            {
                viewModel.Current = new ArticleEntity
                {
                    Title = "文章明细组件",
                    ImageUrl = "~/Modules/Article/Content/Image/Example.png",
                    ArticleContent = "<p>如上图所示，该组件需要一个<code>文章列表组件</code>组合使用，您需要在其它页面添加一个文章列表组件并链接过来，然后点击文章列表中的文章，该组件就可正常显示文章的内容</p>",
                    CreatebyName = "ZKEASOFT"
                };
            }
            return widget.ToWidgetPart(viewModel);
        }
    }
}