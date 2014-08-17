using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Widget;
using Easy.MetaData;
using Easy.CMS.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleListWidgetMeta))]
    public class ArticleListWidget : WidgetBase
    {
        public int ArticleCategory { get; set; }
        public string DetailPageUrl { get; set; }
    }
    class ArticleListWidgetMeta : WidgetMetaData<ArticleListWidget>
    {

        protected override void DataConfigure()
        {
            DataTable("ArticleListWidget");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            InitViewBase();
            ViewConfig(m => m.Title).AsTextBox().Order(4);
            ViewConfig(m => m.ArticleCategory).AsDropDownList().Order(5).DataSource(Constant.DicKeys.ArticleCategory, Constant.SourceType.Dictionary);
            ViewConfig(m => m.DetailPageUrl).AsTextBox().Order(6).AddClass("select").AddProperty("data-url", "/admin/page/select");
        }
    }

}