using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Attribute;
using Easy.Models;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(NavigationEntityMeta))]
    public class NavigationEntity : EditorEntity, IBasicEntity<string>
    {
        public string ID { get; set; }

        public string Title { get; set; }
        public string ParentId { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public int Status { get; set; }
    }
    class NavigationEntityMeta : DataViewMetaData<NavigationEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Navigation");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.ParentId).AsHidden();
            ViewConfig(m => m.Status).AsDropDownList().DataSource(Constant.DicKeys.RecordStatus, Constant.SourceType.Dictionary);
            ViewConfig(m => m.Url).AsTextBox().AddClass("select").AddProperty("data-url", "/admin/page/select");
        }
    }

}