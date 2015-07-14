using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentParagraphMetaData))]
    public class SectionContentParagraph : SectionContent
    {
        public string HtmlContent { get; set; }
    }

    class SectionContentParagraphMetaData : DataViewMetaData<SectionContentParagraph>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionContentParagraph");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }
}