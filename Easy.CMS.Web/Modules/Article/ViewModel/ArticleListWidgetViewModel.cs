using Easy.CMS.Article.Models;
using Easy.Modules.DataDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Article.ViewModel
{
    public class ArticleListWidgetViewModel
    {
        public Data.Pagination Pagin { get; set; }
        public string CategoryTitle { get; set; }
        public ArticleListWidget Widget { get; set; }
        public IEnumerable<ArticleType> ArticleCategory { get; set; }
        public IEnumerable<ArticleEntity> Articles { get; set; }
        public int CurrentCategory { get; set; }
    }
}