using Easy.CMS.Article.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Article.ViewModel
{
    public class ArticleTypeWidgetViewModel
    {
        public ArticleType CurrentType { get; set; }
        public IEnumerable<ArticleType> ArticleTypes { get; set; }
        public string TargetPage { get; set; }
        public int ArticleTypeID { get; set; }
    }
}