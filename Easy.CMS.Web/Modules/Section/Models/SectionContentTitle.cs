using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentTitleMetaData))]
    public class SectionContentTitle : SectionContent
    {
        public string InnerText { get; set; }
        public string Href { get; set; }
    }

    class SectionContentTitleMetaData : DataViewMetaData<SectionContentTitle>
    {
        protected override bool IsIgnoreBase()
        {
            return false;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionContentTitle");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            
        }
    }
}