using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.CMS.Article.Models;
using Easy.Extend;

namespace Easy.CMS.Article.Service
{
    public class ArticleTypeService : ServiceBase<ArticleType>
    {
        public IEnumerable<ArticleType> GetChildren(int Id)
        {
            var articleType = this.Get(Id);
            if (articleType == null) return null;
            return InitChildren(articleType);
        }
        private IEnumerable<ArticleType> InitChildren(ArticleType model)
        {
            IEnumerable<ArticleType> result = Get(new Data.DataFilter().Where("ParentID", OperatorType.Equal, model.ID));
            List<ArticleType> listResult = result.ToList();
            result.Each(m =>
            {
                listResult.AddRange(InitChildren(m));
            });
            return listResult;
        }
    }
}