using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentImageMetaData))]
    public class SectionContentImage : SectionContent
    {
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
        public string ImageTitle { get; set; }
        public string Href { get; set; }
    }

    class SectionContentImageMetaData : DataViewMetaData<SectionContentImage>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionContentImage");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {
            
        }
    }
}