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
            int p = 0;
            int.TryParse(httpContext.Request.QueryString["p"], out p);
            var service = new ProductService();
            var page = new Pagination { PageIndex = p };
            var products = service.Get(new DataFilter().Where("ProductCategory", OperatorType.Equal, pwidget.ProductCategory), page).ToList();
            return widget.ToWidgetPart(new ProductListWidgetViewModel() { Products = products, Page = page });
        }
    }
}