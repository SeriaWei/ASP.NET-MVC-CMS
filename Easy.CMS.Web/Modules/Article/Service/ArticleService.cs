using Easy.Data;
using Easy.CMS.Article.Models;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS.Article.Models;
using Easy.Web.CMS.Article.Service;

namespace Easy.CMS.Article.Service
{
    public class ArticleService : ServiceBase<ArticleEntity>, IArticleService
    {
        public void Publish(long ID)
        {
            this.Update(new ArticleEntity { IsPublish = true, PublishDate = DateTime.Now }, new Data.DataFilter(new List<string> { "IsPublish", "PublishDate" }).Where("ID", OperatorType.Equal, ID));
        }
    }
}