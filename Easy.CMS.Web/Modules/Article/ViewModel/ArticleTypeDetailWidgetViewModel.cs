using Easy.CMS.Article.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Article.ViewModel
{
    public class ArticleTypeDetailWidgetViewModel
    {
        public ArticleType ArticleType { get; set; }
        public IEnumerable<ArticleEntity> Articles { get; set; }

        public long CurrentArticle { get; set; }
    }
}