using Easy.RepositoryPattern;
using Easy.Web.CMS.Article.Models;

namespace Easy.Web.CMS.Article.Service
{
    public interface IArticleService : IService<ArticleEntity>
    {
        void Publish(long ID);
    }
}