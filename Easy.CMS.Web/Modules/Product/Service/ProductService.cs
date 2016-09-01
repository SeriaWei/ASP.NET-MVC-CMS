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
        public void Publish(long ID)
        {
            this.Update(new ProductEntity { IsPublish = true, PublishDate = DateTime.Now }, new DataFilter(new List<string> { "IsPublish", "PublishDate" }).Where("ID", OperatorType.Equal, ID));
        }
    }
}
