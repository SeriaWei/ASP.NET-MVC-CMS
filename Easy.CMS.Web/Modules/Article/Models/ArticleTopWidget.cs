using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Article.Service;
using Easy.CMS.MetaData;
using Easy.CMS.Widget;
using Easy.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleTopWidgetMetaData))]
    public class ArticleTopWidget : WidgetBase
    {
        public int ArticleCategory { get; set; }
        public int Tops { get; set; }
        public string SubTitle { get; set; }
        public string MoreLink { get; set; }
        public string DetailPageUrl { get; set; }
    }
    class ArticleTopWidgetMetaData : WidgetMetaData<ArticleTopWidget>
    {
        protected override void DataConfigure()
        {
            DataTable("ArticleTopWidget");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            InitViewBase();
            ViewConfig(m => m.Title).AsTextBox().Order(4);
            ViewConfig(m => m.SubTitle).AsTextBox().Order(5);
            ViewConfig(m => m.ArticleCategory).AsDropDownList().Order(6).DataSource(new ArticleTypeService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title));
            ViewConfig(m => m.Tops).AsTextBox().Order(7).RegularExpression(Constant.RegularExpression.PositiveIntegers);
            ViewConfig(m => m.MoreLink).AsTextBox().Order(8).AddClass("select").AddProperty("data-url", "/admin/page/select");
            ViewConfig(m => m.DetailPageUrl).AsTextBox().Order(9).AddClass("select").AddProperty("data-url", "/admin/page/select");
     
        }
    }

}