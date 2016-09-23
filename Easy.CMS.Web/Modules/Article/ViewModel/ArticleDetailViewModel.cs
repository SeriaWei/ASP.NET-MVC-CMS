/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Web.CMS.Article.Models;

namespace Easy.CMS.Article.ViewModel
{
    public class ArticleDetailViewModel
    {
        public ArticleEntity Current { get; set; }
        public ArticleEntity Prev { get; set; }
        public ArticleEntity Next { get; set; }
    }
}