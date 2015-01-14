using Easy.CMS.Product.Models;
using Easy.Data;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.CMS.Product.Service
{
    public class ProductService : ServiceBase<Models.Product>
    {
        internal void Publish(long ID)
        {
            this.Update(new Models.Product { IsPublish = true, PublishDate = DateTime.Now }, new Data.DataFilter(new List<string> { "IsPublish", "PublishDate" }).Where("ID", OperatorType.Equal, ID));
        }
    }
}
