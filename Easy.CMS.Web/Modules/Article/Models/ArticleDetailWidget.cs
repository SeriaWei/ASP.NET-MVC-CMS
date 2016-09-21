using System;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleDetailWidgetMetaData)), Serializable]
    public class ArticleDetailWidget : WidgetBase
    {
        public string CustomerClass { get; set; }
    }
    class ArticleDetailWidgetMetaData : WidgetMetaData<ArticleDetailWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.CustomerClass).AsHidden();
        }
    }

}