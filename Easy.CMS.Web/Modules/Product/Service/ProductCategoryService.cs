/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Product.Service
{
    public class ProductCategoryService : ServiceBase<ProductCategory>, IProductCategoryService
    {
        private IProductService _productService;
   
        public IEnumerable<ProductCategory> GetChildren(long id)
        {
            return Get(m => m.ParentID == id);
        }

        public override int Delete(params object[] primaryKeys)
        {
            _productService = _productService ?? ServiceLocator.Current.GetInstance<IProductService>();
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