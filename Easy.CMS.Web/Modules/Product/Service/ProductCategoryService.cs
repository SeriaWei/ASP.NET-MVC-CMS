using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.CMS.Product.Models;
using Easy.Extend;

namespace Easy.CMS.Product.Service
{
    public class ProductCategoryService : ServiceBase<ProductCategory>
    {
        private ProductService _productService;
   
        public IEnumerable<ProductCategory> GetChildren(long id)
        {
            return Get(m => m.ParentID == id);
        }

        public override int Delete(params object[] primaryKeys)
        {
            _productService = _productService ?? new ProductService();
            var item = Get(primaryKeys);
            if (item != null)
            {
                GetChildren(item.ID).Each(m =>
                {
                    _productService.Delete(n => n.ProductCategoryID == m.ID);
                    Delete(m.ID);
                });
                _productService.Delete(n => n.ProductCategoryID == item.ID);
            }
            return base.Delete(primaryKeys);
        }
    }
}