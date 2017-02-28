/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.CMS.Product.Models;
using Easy.CMS.Product.ViewModel;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS.Product.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;
using Easy.Web.CMS;

namespace Easy.CMS.Product.Service
{
    public class ProductCategoryWidgetService : WidgetService<ProductCategoryWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            int category = controllerContext.RouteData.GetCategory();
            ProductCategoryWidget currentWidget = widget as ProductCategoryWidget;
            var categoryService = ServiceLocator.Current.GetInstance<IProductCategoryService>();
            var filter = new DataFilter().Where("ParentID", OperatorType.Equal, currentWidget.ProductCategoryID);
            return widget.ToWidgetPart(new ProductCategoryWidgetViewModel
            {
                Categorys = categoryService.Get(filter),
                CurrentCategory = category,
                TargetPage = currentWidget.TargetPage.IsNullOrEmpty() ? controllerContext.HttpContext.Request.Url.PathAndQuery.ToLower() : currentWidget.TargetPage
            });
        }
    }
}