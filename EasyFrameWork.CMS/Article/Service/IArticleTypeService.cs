/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.RepositoryPattern;
using Easy.Web.CMS.Article.Models;

namespace Easy.Web.CMS.Article.Service
{
    public interface IArticleTypeService : IService<ArticleType>
    {
        IEnumerable<ArticleType> GetChildren(long id);
    }
}