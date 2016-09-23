/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductDetailWidgetMetaData)), Serializable]
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