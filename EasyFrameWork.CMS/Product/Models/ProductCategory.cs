/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Constant;
using Easy.MetaData;
using Easy.Models;

namespace Easy.Web.CMS.Product.Models
{
    [DataConfigure(typeof(ProductCategoryMetaData))]
    public class ProductCategory : EditorEntity
    {
        public int ID { get; set; }

        public int ParentID { get; set; }
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