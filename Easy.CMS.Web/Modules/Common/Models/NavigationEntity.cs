using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Models;
using Easy.Web.CMS;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(NavigationEntityMeta))]
    public class NavigationEntity : EditorEntity
    {
        public string ID { get; set; }
        public int? DisplayOrder { get; set; }

        public string ParentId { get; set; }
        public string Url { get; set; }
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
            ViewConfig(m => m.DisplayOrder).AsHidden();
            ViewConfig(m => m.Url).AsTextBox().AddClass("select").AddProperty("data-url", Urls.SelectPage);
        }
    }

}