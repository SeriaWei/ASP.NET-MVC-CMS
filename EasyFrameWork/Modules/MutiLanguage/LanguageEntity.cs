/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.MetaData;
using Easy.Models;

namespace Easy.Modules.MutiLanguage
{
    [DataConfigure(typeof(LanguageEntityMetaData))]
    public class LanguageEntity
    {
        public string LanKey { get; set; }
        public int LanID { get; set; }
        public string LanValue { get; set; }
        public string Module { get; set; }
        public string LanType { get; set; }
    }
    class LanguageEntityMetaData : DataViewMetaData<LanguageEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("Language");
            DataConfig(m => m.LanKey).Update(false).AsPrimaryKey();
            DataConfig(m => m.LanID).Update(false).AsPrimaryKey();
            DataConfig(m => m.LanType).Update(false);
            DataConfig(m => m.Module).Update(false);
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.LanID).AsTextBox().ReadOnly();
            ViewConfig(m => m.LanKey).AsTextBox().ReadOnly();
            ViewConfig(m => m.LanType).AsTextBox().ReadOnly();
            ViewConfig(m => m.Module).AsTextBox().ReadOnly();
            ViewConfig(m => m.LanValue).AsTextBox().Required();


        }
    }

}
