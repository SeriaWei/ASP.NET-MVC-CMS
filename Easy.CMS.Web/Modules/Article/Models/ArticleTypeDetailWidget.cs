using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.CMS.Article.Service;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleTypeDetailWidgetMetaData))]
    public class ArticleTypeDetailWidget : WidgetBase
    {
        public long ArticleType { get; set; }
    }
    class ArticleTypeDetailWidgetMetaData : WidgetMetaData<ArticleTypeDetailWidget>
    {

        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ArticleType).AsDropDownList().Order(5).DataSource(new ArticleTypeService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title));
        }
    }

}