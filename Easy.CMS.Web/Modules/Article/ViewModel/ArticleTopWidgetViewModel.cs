using System.Collections.Generic;
using Easy.CMS.Article.Models;
using Easy.Web.CMS.Article.Models;

namespace Easy.CMS.Article.ViewModel
{
    public class ArticleTopWidgetViewModel
    {
        public IEnumerable<ArticleEntity> Articles { get; set; }
        public ArticleTopWidget Widget { get; set; }
    }
}