using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.CMS.Product.Service;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductCategoryWidgetMedata))]
    public class ProductCategoryWidget : WidgetBase
    {
        public int? ProductCategoryID { get; set; }
        public string TargetPage { get; set; }
    }

    class ProductCategoryWidgetMedata : WidgetMetaData<ProductCategoryWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ProductCategoryID).AsDropDownList().DataSource(() =>
            {
                return new ProductCategoryService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title);
            });
            ViewConfig(m => m.TargetPage).AsTextBox().AddClass("select").AddProperty("data-url", "/admin/page/select");
        }
    }
}