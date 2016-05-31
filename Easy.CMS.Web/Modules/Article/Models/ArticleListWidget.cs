using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS.Widget;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.CMS.Article.Service;
using Easy.Web.CMS;
using Easy.Web.CMS.Article.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleListWidgetMeta))]
    public class ArticleListWidget : WidgetBase
    {
        public int ArticleTypeID { get; set; }
        public string DetailPageUrl { get; set; }
        public bool IsPageable { get; set; }
        public int? PageSize { get; set; }
    }
    class ArticleListWidgetMeta : WidgetMetaData<ArticleListWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            var articleTypeService = ServiceLocator.Current.GetInstance<IArticleTypeService>();
            ViewConfig(m => m.ArticleTypeID).AsDropDownList().Order(NextOrder()).DataSource(articleTypeService.Get().ToDictionary(m => m.ID.ToString(), m => m.Title)).Required();
            ViewConfig(m => m.DetailPageUrl).AsTextBox().Order(NextOrder()).AddClass("select").AddProperty("data-url", Urls.SelectPage);
        }
    }

}