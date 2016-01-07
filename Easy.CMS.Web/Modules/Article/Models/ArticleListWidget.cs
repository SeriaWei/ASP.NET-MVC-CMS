using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS.Widget;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.CMS.Article.Service;
using Easy.Web.CMS;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleListWidgetMeta))]
    public class ArticleListWidget : WidgetBase
    {
        public int? ArticleCategoryID { get; set; }
        public string DetailPageUrl { get; set; }
        public bool IsPageable { get; set; }
        public int? PageSize { get; set; }
    }
    class ArticleListWidgetMeta : WidgetMetaData<ArticleListWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Title).AsTextBox().Order(4);
            ViewConfig(m => m.ArticleCategoryID).AsDropDownList().Order(5).DataSource(new ArticleTypeService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title));
            ViewConfig(m => m.DetailPageUrl).AsTextBox().Order(6).AddClass("select").AddProperty("data-url", Urls.SelectPage);
        }
    }

}