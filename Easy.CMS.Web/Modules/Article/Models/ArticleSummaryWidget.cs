using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleSummaryWidgetMetaData))]
    public class ArticleSummaryWidget : WidgetBase
    {
        public string SubTitle { get; set; }
        public string DetailLink { get; set; }
        public string Summary { get; set; }
    }
    class ArticleSummaryWidgetMetaData : WidgetMetaData<ArticleSummaryWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Title).AsTextBox().Order(4);
            ViewConfig(m => m.SubTitle).AsTextBox().Order(5);
            ViewConfig(m => m.DetailLink).AsTextBox().Order(6).AddClass("select").AddProperty("data-url", "/admin/page/select");
            ViewConfig(m => m.Summary).AsMutiLineTextBox().Order(7).AddClass("html");
        }
    }

}