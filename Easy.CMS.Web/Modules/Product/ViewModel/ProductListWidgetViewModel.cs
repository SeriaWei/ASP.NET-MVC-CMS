/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.Data;
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