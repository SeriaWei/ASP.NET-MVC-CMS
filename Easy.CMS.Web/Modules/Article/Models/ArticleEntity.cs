using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Models;
using Easy.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArticleEntityMeta))]
    public class ArticleEntity : EditorEntity, IBasicEntity<long>, IImage
    {
        public long ID { get; set; }

        public string Title { get; set; }
        public string Summary { get; set; }
        public string MetaKeyWords { get; set; }
        public string MetaDescription { get; set; }
        public int Counter { get; set; }
        public string Description { get; set; }
        public string ArticleContent { get; set; }
        public int Status { get; set; }

        public string ImageThumbUrl { get; set; }

        public string ImageUrl { get; set; }

        public int ArticleCategory { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool IsPublish { get; set; }
    }
    class ArticleEntityMeta : DataViewMetaData<ArticleEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Article");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Status).AsDropDownList().DataSource(Constant.DicKeys.RecordStatus, Constant.SourceType.Dictionary);
            ViewConfig(m => m.ImageThumbUrl).AsTextBox().AddClass("select").HideInGrid();
            ViewConfig(m => m.ImageUrl).AsTextBox().AddClass("select").HideInGrid();
            ViewConfig(m => m.ArticleCategory).AsDropDownList().DataSource(Constant.DicKeys.ArticleCategory, Constant.SourceType.Dictionary);
            ViewConfig(m => m.ArticleContent).AsMutiLineTextBox().AddClass("html").HideInGrid();
            ViewConfig(m => m.PublishDate).AsTextBox().Hide();
            ViewConfig(m => m.IsPublish).AsTextBox().Hide();
        }
    }

}