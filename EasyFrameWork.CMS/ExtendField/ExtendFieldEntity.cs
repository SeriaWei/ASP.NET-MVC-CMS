/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.MetaData;
using Easy.Models;

namespace Easy.Web.CMS.ExtendField
{
    [DataConfigure(typeof(ExtendFieldEntityMetaData)), Serializable]
    public class ExtendFieldEntity : EditorEntity
    {
        public int? ID { get; set; }
        public string OwnerModule { get; set; }
        public string OwnerID { get; set; }
        public string Value { get; set; }
    }

    class ExtendFieldEntityMetaData : DataViewMetaData<ExtendFieldEntity>
    {

        protected override void DataConfigure()
        {
            DataTable("ExtendField");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.OwnerModule).AsHidden();
            ViewConfig(m => m.OwnerID).AsHidden();
            ViewConfig(m => m.Description).AsTextBox();
        }
    }
}