using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.CMS.Widget;
using Easy.Data;
using Easy.Modules.DataDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;

namespace Easy.CMS.Article.Service
{
    public class ArticleTopWidgetService : WidgetService<ArticleTopWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var currentWidget = widget as ArticleTopWidget;
            var articleTypeService = new ArticleTypeService();

            var categorys = articleTypeService.GetChildren(currentWidget.ArticleCategory);

            var page = new Pagination
            {
                PageIndex = 0,
                PageSize = currentWidget.Tops
            };
            var viewModel = new ArticleTopWidgetViewModel
            {
                Widget = currentWidget
            };
            var filter = new DataFilter();
            filter.Where("IsPublish=true");
            filter.OrderBy("PublishDate", Constant.OrderType.Descending);
            viewModel.Articles = new ArticleService().Get(filter.Where("ArticleCategory", Constant.OperatorType.In, categorys.ToList(m => m.ID)), page);
            return widget.ToWidgetPart(viewModel);
        }
    }
}