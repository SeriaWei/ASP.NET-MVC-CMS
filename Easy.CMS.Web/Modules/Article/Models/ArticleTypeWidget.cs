using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.CMS.Article.Service;
using Easy.Web.CMS;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleTypeWidgetMetaData))]
    public class ArticleTypeWidget : WidgetBase
    {
        public int? ArticleTypeID { get; set; }
        public string TargetPage { get; set; }
    }
    class ArticleTypeWidgetMetaData : WidgetMetaData<ArticleTypeWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ArticleTypeID).AsDropDownList().DataSource(() =>
            {
                return new ArticleTypeService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title);
            });
            ViewConfig(m => m.TargetPage).AsTextBox().AddClass("select").AddProperty("data-url", Urls.SelectPage);
        }
    }
}