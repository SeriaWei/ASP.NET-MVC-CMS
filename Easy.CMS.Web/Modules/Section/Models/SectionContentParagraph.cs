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
            DataConfig(m => m.SectionWidgetId).Insert(true).Update(false);
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.HtmlContent).AsMutiLineTextBox().AddClass("html");
        }
    }
}