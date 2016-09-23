/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.Web;
using Easy.CMS.Product.Models;
using Easy.CMS.Product.ViewModel;
using Easy.Data;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

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
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            ProductListWidget pwidget = widget as ProductListWidget;
            var filter = new DataFilter();
            filter.Where("IsPublish", OperatorType.Equal, true);
            filter.OrderBy("CreateDate", OrderType.Descending);
            int p;
            int.TryParse(httpContext.Request.QueryString["p"], out p);
            int c;
            if (int.TryParse(httpContext.Request.QueryString["pc"], out c))
            {
                filter.Where("ProductCategoryID", OperatorType.Equal, c);
            }
            else
            {
                filter.Where("ProductCategoryID", OperatorType.Equal, pwidget.ProductCategoryID);
            }


            var service = ServiceLocator.Current.GetInstance<IProductService>();
            IEnumerable<ProductEntity> products = null;
            var page = new Pagination { PageIndex = p, PageSize = pwidget.PageSize ?? 20 };
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