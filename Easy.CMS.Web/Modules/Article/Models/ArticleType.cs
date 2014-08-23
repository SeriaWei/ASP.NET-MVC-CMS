using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Models;
using Easy.RepositoryPattern;
using Easy.MetaData;

namespace Easy.CMS.Article.Models
{
    [DataConfigure(typeof(ArtycleTypeMetaData))]
    public class ArticleType : EditorEntity
    {
        public long ID { get; set; }

        public long ParentID { get; set; }
    }
    class ArtycleTypeMetaData : DataViewMetaData<ArticleType>
    {
        protected override void DataConfigure()
        {
            DataTable("ArticleType");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.ParentID).AsHidden();
            ViewConfig(m => m.Status).AsDropDownList().DataSource(Constant.DicKeys.RecordStatus, Constant.SourceType.Dictionary);
        }
    }

}