using Easy.CMS.Product.Models;
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
        public IEnumerable<Models.Product> Products { get; set; }
        public string CustomerStyle { get; set; }
        public string DetailPageUrl { get; set; }
        public bool IsPageable { get; set; }
    }
}