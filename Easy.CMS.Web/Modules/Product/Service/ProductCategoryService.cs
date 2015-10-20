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
        public override void Add(ProductCategory item)
        {
            item.ParentID = item.ParentID ?? 0;
            base.Add(item);
        }

        public IEnumerable<ProductCategory> GetChildren(int Id)
        {
            var category = Get(Id);
            if (category == null) return null;
            return InitChildren(category);
        }
        private IEnumerable<ProductCategory> InitChildren(ProductCategory model)
        {
            IEnumerable<ProductCategory> result = Get(new DataFilter().Where("ParentID", OperatorType.Equal, model.ID));
            List<ProductCategory> listResult = result.ToList();
            result.Each(m => listResult.AddRange(InitChildren(m)));
            return listResult;
        }
    }
}