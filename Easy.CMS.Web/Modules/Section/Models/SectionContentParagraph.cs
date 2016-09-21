using System;
using Easy.MetaData;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentParagraphMetaData)), Serializable]
    public class SectionContentParagraph : SectionContent
    {
        public string HtmlContent { get; set; }
        public override int SectionContentType
        {
            get
            {
                return (int)Types.Paragraph;
            }
        }
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
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.SectionWidgetId).Insert(true).Update(false);
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.HtmlContent).AsTextArea().AddClass("html");
        }
    }
}