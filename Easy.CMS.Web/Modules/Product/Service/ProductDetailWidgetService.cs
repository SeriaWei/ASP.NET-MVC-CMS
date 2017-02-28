/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.CMS.Product.Models;
using Easy.Web.CMS;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;

namespace Easy.CMS.Product.Service
{
    public class ProductDetailWidgetService : WidgetService<ProductDetailWidget>
    {
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            long productId = 0;
            if (controllerContext.RouteData.Values.ContainsKey("post"))
            {
                long.TryParse(controllerContext.RouteData.Values["post"].ToString(), out productId);
            }
            var service = ServiceLocator.Current.GetInstance<IProductService>();
            var product = service.Get(productId) ?? new ProductEntity
            {
                Title = "产品明细组件使用说明",
                ImageUrl = "~/Modules/Product/Content/Image/Example.png",
                ProductContent = "<p>如上图所示，该组件需要一个<code>产品列表组件</code>组合使用，您需要在其它页面添加一个产品列表组件并链接过来，然后点击产品列表中的产品，该组件就可正常显示产品的内容</p>",
                CreatebyName = "ZKEASOFT"
            };

            var page = controllerContext.HttpContext.GetLayout().Page;
            page.MetaDescription = product.SEODescription;
            page.MetaKeyWorlds = product.SEOKeyWord;
            page.Title = product.SEOTitle ?? product.Title;

            return widget.ToWidgetPart(product);
        }
    }
}