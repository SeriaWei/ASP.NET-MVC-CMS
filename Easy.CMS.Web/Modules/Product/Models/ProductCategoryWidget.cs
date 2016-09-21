using System;
using System.Linq;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Product.Service;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductCategoryWidgetMedata)), Serializable]
    public class ProductCategoryWidget : WidgetBase
    {
        public int ProductCategoryID { get; set; }
        public string TargetPage { get; set; }
    }

    class ProductCategoryWidgetMedata : WidgetMetaData<ProductCategoryWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ProductCategoryID).AsDropDownList().Order(NextOrder()).DataSource(() =>
            {
                return ServiceLocator.Current.GetInstance<IProductCategoryService>().Get().ToDictionary(m => m.ID.ToString(), m => m.Title);
            }).Required();
            ViewConfig(m => m.TargetPage).AsHidden();
        }
    }
}