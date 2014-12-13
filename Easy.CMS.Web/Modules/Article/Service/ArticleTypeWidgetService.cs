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
    public class ArticleTypeWidgetService : WidgetService<ArticleTypeWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            ArticleTypeWidget currentWidget = widget as ArticleTypeWidget;
            var service = new ArticleTypeService();
            var filter = new Data.DataFilter().Where("ParentID", Data.OperatorType.Equal, currentWidget.ArticleTypeID);
            int ac = 0;
            int.TryParse(httpContext.Request.QueryString["ac"], out ac);
            return widget.ToWidgetPart(new ArticleTypeWidgetViewModel
            {
                ArticleTypes = service.Get(filter),
                CurrentType = service.Get(currentWidget.ArticleTypeID),
                TargetPage = currentWidget.TargetPage.IsNullOrEmpty() ? httpContext.Request.Path.ToLower() : currentWidget.TargetPage,
                ArticleTypeID = ac
            });
        }
    }
}