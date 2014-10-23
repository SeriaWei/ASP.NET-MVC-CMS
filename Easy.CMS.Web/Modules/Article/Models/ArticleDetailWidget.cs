using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleDetailWidgetMetaData))]
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