using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS;
using Easy.Web.CMS.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleSummaryWidgetMetaData))]
    public class ArticleSummaryWidget : WidgetBase
    {
        public string SubTitle { get; set; }
        public string DetailLink { get; set; }
        public string Summary { get; set; }
        public string Style { get; set; }
    }
    class ArticleSummaryWidgetMetaData : WidgetMetaData<ArticleSummaryWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Title).AsTextBox().Order(4);
            ViewConfig(m => m.SubTitle).AsTextBox().Order(5);
            ViewConfig(m => m.Style).AsDropDownList().Order(6)
                .DataSource(() =>
                    new Dictionary<string, string> { 
                    { "bs-callout-default", "默认" },
                    { "bs-callout-danger", "危险" }, 
                    { "bs-callout-warning", "警告" }, 
                    { "bs-callout-info", "信息" } ,
                    { "bs-callout-success", "成功" } 
            }); ;
            ViewConfig(m => m.DetailLink).AsTextBox().Order(7).AddClass("select").AddProperty("data-url", Urls.SelectPage);
            ViewConfig(m => m.Summary).AsTextArea().Order(8).AddClass("html");
        }
    }

}