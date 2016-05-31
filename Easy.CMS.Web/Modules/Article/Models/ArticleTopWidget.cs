using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Article.Service;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using Easy.MetaData;
using Easy.Web.CMS;
using Easy.Web.CMS.Article.Service;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleTopWidgetMetaData))]
    public class ArticleTopWidget : WidgetBase
    {
        public int ArticleTypeID { get; set; }
        public int? Tops { get; set; }
        public string SubTitle { get; set; }
        public string MoreLink { get; set; }
        public string DetailPageUrl { get; set; }
    }
    class ArticleTopWidgetMetaData : WidgetMetaData<ArticleTopWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.SubTitle).AsTextBox().Order(NextOrder());
            var articleTypeService = ServiceLocator.Current.GetInstance<IArticleTypeService>();
            ViewConfig(m => m.ArticleTypeID).AsDropDownList().Order(NextOrder()).DataSource(articleTypeService.Get().ToDictionary(m => m.ID.ToString(), m => m.Title)).Required();
            ViewConfig(m => m.Tops).AsTextBox().Order(NextOrder()).RegularExpression(Easy.Constant.RegularExpression.PositiveIntegers);
            ViewConfig(m => m.MoreLink).AsTextBox().Order(NextOrder()).AddClass("select").AddProperty("data-url", Urls.SelectPage);
            ViewConfig(m => m.DetailPageUrl).AsTextBox().Order(NextOrder()).AddClass("select").AddProperty("data-url", Urls.SelectPage);
     
        }
    }

}