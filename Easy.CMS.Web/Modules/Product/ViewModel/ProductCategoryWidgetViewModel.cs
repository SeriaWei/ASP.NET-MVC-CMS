using System.Collections.Generic;
using Easy.Web.CMS.Product.Models;

namespace Easy.CMS.Product.ViewModel
{
    public class ProductCategoryWidgetViewModel
    {
        public IEnumerable<ProductCategory> Categorys { get; set; }
        public int CurrentCategory { get; set; }
        public string TargetPage { get; set; }
    }
}