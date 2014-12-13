using Easy.Web.CMS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using Easy.CMS.Product.Models;
using Easy.Web.CMS.Widget;
using Easy.Data;
using Easy.CMS.Product.ViewModel;

namespace Easy.CMS.Product.Service
{
    public class ProductListWidgetService : WidgetService<ProductListWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            ProductListWidget pwidget = widget as ProductListWidget;
            var filter = new DataFilter();
            int p = 0;
            int.TryParse(httpContext.Request.QueryString["p"], out p);
            int c = 0;
            if (int.TryParse(httpContext.Request.QueryString["pc"], out c))
            {
                filter.Where("ProductCategory", OperatorType.Equal, c);
            }
            else if (pwidget.ProductCategoryID.HasValue)
            {
                filter.Where("ProductCategory", OperatorType.Equal, pwidget.ProductCategoryID);
            }
            var service = new ProductService();
            IEnumerable<Models.Product> products = null;
            var page = new Pagination { PageIndex = p };
            if (pwidget.IsPageable)
            {
                products = service.Get(filter, page);
            }
            else
            {
                products = service.Get(filter);
            }
            return widget.ToWidgetPart(new ProductListWidgetViewModel()
            {
                Products = products,
                Page = page,
                IsPageable = pwidget.IsPageable
            });
        }
    }
}