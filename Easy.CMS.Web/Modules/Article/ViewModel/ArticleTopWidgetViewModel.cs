using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Article.Models;

namespace Easy.CMS.Article.ViewModel
{
    public class ArticleTopWidgetViewModel
    {
        public IEnumerable<ArticleEntity> Articles { get; set; }
        public ArticleTopWidget Widget { get; set; }
    }
}