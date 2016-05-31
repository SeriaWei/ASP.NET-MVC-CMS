using Easy.CMS.Product.Models;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Extend;
using Easy.Web.CMS.Product.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Product.Service
{
    public class ProductDetailWidgetService : WidgetService<ProductDetailWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            long productId = 0;
            long.TryParse(httpContext.Request.QueryString["id"], out productId);
            var service = ServiceLocator.Current.GetInstance<IProductService>();
            var product= service.Get(productId);
            if (product != null)
            {
                var page = httpContext.GetLayout().Page;
                if (product.SEODescription.IsNotNullAndWhiteSpace())
                {
                    page.MetaDescription = product.SEODescription;
                }
                if (product.SEOKeyWord.IsNotNullAndWhiteSpace())
                {
                    page.MetaKeyWorlds = product.SEOKeyWord;
                }
                if (product.SEOTitle.IsNotNullAndWhiteSpace())
                {
                    page.Title = product.SEOTitle;
                }
            }          

            return widget.ToWidgetPart(product);
        }
    }
}