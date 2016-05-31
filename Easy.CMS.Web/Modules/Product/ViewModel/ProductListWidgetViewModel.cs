using Easy.CMS.Product.Models;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS.Product.Models;

namespace Easy.CMS.Product.ViewModel
{
    public class ProductListWidgetViewModel
    {
        public Pagination Page { get; set; }
        public IEnumerable<ProductEntity> Products { get; set; }
        public string Columns { get; set; }
        public string DetailPageUrl { get; set; }
        public bool IsPageable { get; set; }
    }
}