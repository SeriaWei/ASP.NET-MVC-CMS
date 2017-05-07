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
            ProductListWidget pwidget = widget as ProductListWidget;
            var filter = new DataFilter();
            filter.Where("IsPublish", OperatorType.Equal, true);
            filter.OrderBy("ID", OrderType.Descending);

            int pageIndex = controllerContext.RouteData.GetPage();
            int category = controllerContext.RouteData.GetCategory();

            if (category > 0)
            {
                filter.Where("ProductCategoryID", OperatorType.Equal, category);
            }
            else
            {
                var categoryService = ServiceLocator.Current.GetInstance<IProductCategoryService>();
                var ids = categoryService.Get(new DataFilter().Where("ParentID", OperatorType.Equal, pwidget.ProductCategoryID)).Select(m => m.ID);
                if (ids.Any())
                {
                    filter.Where("ProductCategoryID", OperatorType.In, ids.Concat(new[] { pwidget.ProductCategoryID }));
                }
                else
                {
                    filter.Where("ProductCategoryID", OperatorType.Equal, pwidget.ProductCategoryID);
                }
            }


            var service = ServiceLocator.Current.GetInstance<IProductService>();
            IEnumerable<ProductEntity> products = null;
            var page = new Pagination { PageIndex = pageIndex, PageSize = pwidget.PageSize ?? 20 };
            if (pwidget.IsPageable)
            {
                products = service.Get(filter, page);
            }
            else
            {
                products = service.Get(filter);
            }
            return widget.ToWidgetPart(new ProductListWidgetViewModel
            {
                Products = products,
                Page = page,
                IsPageable = pwidget.IsPageable,
                Columns = pwidget.Columns,
                DetailPageUrl = pwidget.DetailPageUrl
            });
        }
    }
}