using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Constant;
using Easy.Models;
using Easy.MetaData;
using Easy.Web.CMS.ExtendField;

namespace Easy.Web.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleEntityMeta))]
    public class ArticleEntity : EditorEntity, IImage, IExtendField
    {
        public long ID { get; set; }

        public string Summary { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public int? Counter { get; set; }
        public string ArticleContent { get; set; }

        public string ImageThumbUrl { get; set; }

        public string ImageUrl { get; set; }

        public int? ArticleTypeID { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }

        public IEnumerable<ExtendFieldEntity> ExtendFields
        {
            get;set;
        }
    }
    class ArticleEntityMeta : DataViewMetaData<ArticleEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Article");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.ID).Update(false);
            DataConfig(m => m.ExtendFields).SetReference<ExtendFieldEntity, IExtendFieldService>((article, field) => field.OwnerModule == TargetType.Name && field.OwnerID == article.ID.ToString());
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required().Order(1);
            ViewConfig(m => m.Status).AsDropDownList().DataSource(DicKeys.RecordStatus, SourceType.Dictionary);
            ViewConfig(m => m.ImageThumbUrl).AsTextBox().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.ImageUrl).AsTextBox().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.ArticleTypeID).AsDropDownList().DataSource(ViewDataKeys.ArticleCategory, SourceType.ViewData).Required();
            ViewConfig(m => m.ArticleContent).AsTextArea().AddClass(StringKeys.HtmlEditorClass).HideInGrid();
            ViewConfig(m => m.PublishDate).AsTextBox().Hide();
            ViewConfig(m => m.IsPublish).AsTextBox().Hide();
        }
    }

}