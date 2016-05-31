using Easy.CMS.Product.Models;
using Easy.CMS.Product.ViewModel;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;
using Easy.Web.CMS.Product.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Product.Service
{
    public class ProductCategoryWidgetService : WidgetService<ProductCategoryWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            ProductCategoryWidget currentWidget = widget as ProductCategoryWidget;
            int c;
            var categoryService = ServiceLocator.Current.GetInstance<IProductCategoryService>();
            int.TryParse(httpContext.Request.QueryString["pc"], out c);
            var filter = new Data.DataFilter().Where("ParentID", Data.OperatorType.Equal, currentWidget.ProductCategoryID);
            return widget.ToWidgetPart(new ProductCategoryWidgetViewModel
            {
                Categorys = categoryService.Get(filter),
                CurrentCategory = c,
                TargetPage = currentWidget.TargetPage.IsNullOrEmpty() ? httpContext.Request.Url.PathAndQuery.ToLower() : currentWidget.TargetPage
            });
        }
    }
}