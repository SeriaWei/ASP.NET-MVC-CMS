using Easy.CMS.Article.ActionFilter;
using Easy.CMS.Article.Controllers;
using Easy.Web.Filter;
namespace Easy.CMS.Article
{
    public class FilterConfig : ConfigureFilterBase
    {
        public FilterConfig(IFilterRegister register)
            : base(register)
        {
        }
        public override void Configure()
        {
            Registry.Register<ArticleController, ViewDataArticleTypeAttribute>(m => m.Index());
            Registry.Register<ArticleController, ViewDataArticleTypeAttribute>(m => m.Create());
            Registry.Register<ArticleController, ViewDataArticleTypeAttribute>(m => m.Create(null));
            Registry.Register<ArticleController, ViewDataArticleTypeAttribute>(m => m.Edit(0));
            Registry.Register<ArticleController, ViewDataArticleTypeAttribute>(m => m.Edit(null));
            Registry.Register<ArticleController, ViewDataArticleTypeAttribute>(m => m.GetList());
        }

    }
}