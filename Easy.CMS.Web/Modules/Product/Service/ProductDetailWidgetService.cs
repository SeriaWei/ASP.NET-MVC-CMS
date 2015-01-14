using Easy.CMS.Product.Models;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Product.Service
{
    public class ProductDetailWidgetService : WidgetService<ProductDetailWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            long productId = 0;
            long.TryParse(httpContext.Request.QueryString["id"], out productId);
            var service = new ProductService();
            return widget.ToWidgetPart(service.Get(productId));
        }
    }
}