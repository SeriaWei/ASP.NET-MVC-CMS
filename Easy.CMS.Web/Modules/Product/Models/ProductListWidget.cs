using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.CMS.Product.Service;
using Easy.Extend;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductListWidgetMetaData))]
    public class ProductListWidget : WidgetBase
    {
        public bool IsPageable { get; set; }
        public int? ProductCategoryID { get; set; }
        public string DetailPageUrl { get; set; }
    }

    class ProductListWidgetMetaData : WidgetMetaData<ProductListWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ProductCategoryID).AsDropDownList().DataSource(() =>
            {
                var dicts = new Dictionary<string, string>();
                dicts.Add("", "所有类别");
                new ProductCategoryService().Get().Each(m => { dicts.Add(m.ID.ToString(), m.Title); });
                return dicts;
            });
            ViewConfig(m => m.DetailPageUrl).AsTextBox().AddClass("select").AddProperty("data-url", "/admin/page/select");
        }
    }

}