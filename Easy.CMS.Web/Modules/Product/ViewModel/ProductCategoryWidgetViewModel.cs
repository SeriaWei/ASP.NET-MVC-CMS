using Easy.CMS.Product.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Product.ViewModel
{
    public class ProductCategoryWidgetViewModel
    {
        public IEnumerable<ProductCategory> Categorys { get; set; }
        public int CurrentCategory { get; set; }
        public string TargetPage { get; set; }
    }
}