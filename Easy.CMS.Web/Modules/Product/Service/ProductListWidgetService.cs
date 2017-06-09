/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Product.Models;
using Easy.CMS.Product.ViewModel;
using Easy.Data;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;
using Easy.Web.CMS;

namespace Easy.CMS.Product.Service
{
    public class ProductListWidgetService : WidgetService<ProductListWidget>
    {
        public override void Add(ProductListWidget item)
        {
            if (!item.PageSize.HasValue || item.PageSize.Value == 0)
            {
                item.PageSize = 20;
            }
            base.Add(item);
        }
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            ProductListWidget currentWidget = widget as ProductListWidget;
            var filter = new DataFilter();
            filter.Where("IsPublish", OperatorType.Equal, true);
            filter.OrderBy("CreateDate", OrderType.Descending);
            var categoryService = ServiceLocator.Current.GetInstance<IProductCategoryService>();

            int pageIndex = controllerContext.RouteData.GetPage();
            int category = controllerContext.RouteData.GetCategory();

            if (category > 0)
            {
                filter.Where("ProductCategoryID", OperatorType.Equal, category);
            }
            else
            {
                var ids = categoryService.Get(new DataFilter().Where("ParentID", OperatorType.Equal, currentWidget.ProductCategoryID)).Select(m => m.ID);
                if (ids.Any())
                {
                    filter.Where("ProductCategoryID", OperatorType.In, ids.Concat(new[] { currentWidget.ProductCategoryID }));
                }
                else
                {
                    filter.Where("ProductCategoryID", OperatorType.Equal, currentWidget.ProductCategoryID);
                }
            }


            var service = ServiceLocator.Current.GetInstance<IProductService>();
            IEnumerable<ProductEntity> products = null;
            var pagin = new Pagination { PageIndex = pageIndex, PageSize = currentWidget.PageSize ?? 20 };
            if (currentWidget.IsPageable)
            {
                products = service.Get(filter, pagin);
            }
            else
            {
                products = service.Get(filter);
            }

            var categoryEntity = categoryService.Get(category == 0 ? currentWidget.ProductCategoryID : category);
            if (categoryEntity != null)
            {
                var page = controllerContext.HttpContext.GetLayout().Page;
                page.Title = (page.Title ?? "") + " - " + categoryEntity.Title;
            }

            return widget.ToWidgetPart(new ProductListWidgetViewModel
            {
                Products = products,
                Page = pagin,
                IsPageable = currentWidget.IsPageable,
                Columns = currentWidget.Columns,
                DetailPageUrl = currentWidget.DetailPageUrl
            });
        }
    }
}