using Easy.CMS.Product.Models;
using Easy.Data;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Web.CMS.ExtendField;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;

namespace Easy.CMS.Product.Service
{
    public class ProductService : ServiceBase<ProductEntity>, IProductService
    {
        private readonly IExtendFieldService _extendFieldService;
        public const string FieldFromCategory = "ProductFieldFromCategory";
        public ProductService(IExtendFieldService extendFieldService)
        {
            _extendFieldService = extendFieldService;
        }

        public override void Add(ProductEntity item)
        {
            base.Add(item);

            if (item.CategoryFields != null)
            {
                foreach (var field in item.CategoryFields)
                {
                    field.OwnerModule = FieldFromCategory;
                    if (item.ID.HasValue)
                    {
                        field.OwnerID = item.ID.Value.ToString();
                    }
                    _extendFieldService.Add(field);
                }
            }
        }

        public override bool Update(ProductEntity item, params object[] primaryKeys)
        {
            if (item.CategoryFields != null)
            {
                foreach (var field in item.CategoryFields)
                {
                    _extendFieldService.Update(field);
                }
            }
            return base.Update(item, primaryKeys);
        }

        public override ProductEntity Get(params object[] primaryKeys)
        {
            var product = base.Get(primaryKeys);
            product.CategoryFields =
                _extendFieldService.Get(
                    new DataFilter().Where("OwnerModule", OperatorType.Equal, FieldFromCategory)
                        .Where("OwnerID", OperatorType.Equal, product.ID));
            return product;
        }

        public void Publish(long ID)
        {
            this.Update(new ProductEntity { IsPublish = true, PublishDate = DateTime.Now }, new DataFilter(new List<string> { "IsPublish", "PublishDate" }).Where("ID", OperatorType.Equal, ID));
        }
    }
}
