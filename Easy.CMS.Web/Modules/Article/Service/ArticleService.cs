using Easy.Data;
using Easy.CMS.Article.Models;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Article.Service
{
    public class ArticleService : ServiceBase<ArticleEntity>
    {
        public void Publish(long ID)
        {
            this.Update(new ArticleEntity { IsPublish = true, PublishDate = DateTime.Now }, new Data.DataFilter(new List<string> { "IsPublish", "PublishDate" }).Where("ID", OperatorType.Equal, ID));
        }
    }
}