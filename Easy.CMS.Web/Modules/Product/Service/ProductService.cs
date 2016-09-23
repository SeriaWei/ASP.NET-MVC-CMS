/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Product.Models;
using Easy.Web.CMS.Product.Service;

namespace Easy.CMS.Product.Service
{
    public class ProductService : ServiceBase<ProductEntity>, IProductService
    {
        public void Publish(long ID)
        {
            Update(new ProductEntity { IsPublish = true, PublishDate = DateTime.Now }, new DataFilter(new List<string> { "IsPublish", "PublishDate" }).Where("ID", OperatorType.Equal, ID));
        }
    }
}
