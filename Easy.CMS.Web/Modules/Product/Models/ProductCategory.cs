using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Constant;
using Easy.Models;
using Easy.RepositoryPattern;
using Easy.MetaData;

namespace Easy.CMS.Product.Models
{
    [DataConfigure(typeof(ProductCategoryMetaData))]
    public class ProductCategory : EditorEntity
    {
        public long ID { get; set; }

        public long ParentID { get; set; }
    }
    class ProductCategoryMetaData : DataViewMetaData<ProductCategory>
    {
        protected override void DataConfigure()
        {
            DataTable("ProductCategory");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.ParentID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().MaxLength(200).Required();
            ViewConfig(m => m.Status).AsDropDownList().DataSource(DicKeys.RecordStatus, SourceType.Dictionary);
        }
    }

}