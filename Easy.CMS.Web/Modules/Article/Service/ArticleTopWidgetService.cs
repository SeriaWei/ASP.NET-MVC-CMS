/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.CMS.Article.Models;
using Easy.CMS.Article.ViewModel;
using Easy.Data;
using Easy.Web.CMS.Article.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;
using System.Linq;

namespace Easy.CMS.Article.Service
{
    public class ArticleTopWidgetService : WidgetService<ArticleTopWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
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
            filter.OrderBy("CreateDate", OrderType.Descending);
            var articleTypeService = ServiceLocator.Current.GetInstance<IArticleTypeService>();

            var ids = articleTypeService.Get(new DataFilter().Where("ParentID", OperatorType.Equal, currentWidget.ArticleTypeID)).Select(m => m.ID);
            if (ids.Any())
            {
                filter.Where("ArticleTypeID", OperatorType.In, ids.Concat(new[] { currentWidget.ArticleTypeID }));
            }
            else
            {
                filter.Where("ArticleTypeID", OperatorType.Equal, currentWidget.ArticleTypeID);
            }
            
            viewModel.Articles = ServiceLocator.Current.GetInstance<IArticleService>().Get(filter, page);
            return widget.ToWidgetPart(viewModel);
        }
    }
}