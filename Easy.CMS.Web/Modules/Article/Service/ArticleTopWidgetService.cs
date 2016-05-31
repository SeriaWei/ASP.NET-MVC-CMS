using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Web.CMS.Widget;
using Easy.Data;
using Easy.Modules.DataDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;
using Easy.Web.CMS.Article.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Article.Service
{
    public class ArticleTopWidgetService : WidgetService<ArticleTopWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var currentWidget = widget as ArticleTopWidget;
            var page = new Pagination
            {
                PageIndex = 0,
                PageSize = currentWidget.Tops ?? 20
            };
            var viewModel = new ArticleTopWidgetViewModel
            {
                Widget = currentWidget
            };
            var filter = new DataFilter();
            filter.Where("IsPublish", OperatorType.Equal, true);
            filter.OrderBy("PublishDate", OrderType.Descending);
            filter.Where("ArticleTypeID", OperatorType.Equal, currentWidget.ArticleTypeID);
            viewModel.Articles = ServiceLocator.Current.GetInstance<IArticleService>().Get(filter, page);
            return widget.ToWidgetPart(viewModel);
        }
    }
}