using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductDetailWidgetMetaData))]
    public class ProductDetailWidget : WidgetBase
    {
        public string CustomerClass { get; set; }
    }
    class ProductDetailWidgetMetaData : WidgetMetaData<ProductDetailWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.CustomerClass).AsHidden();
        }
    }
}