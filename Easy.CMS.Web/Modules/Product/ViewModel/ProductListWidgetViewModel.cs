using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Product.ViewModel
{
    public class ProductListWidgetViewModel
    {
        public Pagination Page { get; set; }
        public List<Product.Models.Product> Products { get; set; }
    }
}