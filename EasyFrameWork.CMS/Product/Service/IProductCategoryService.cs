/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Product.Models;

namespace Easy.Web.CMS.Product.Service
{
    public interface IProductCategoryService : IService<ProductCategory>
    {
        IEnumerable<ProductCategory> GetChildren(long id);
    }
}