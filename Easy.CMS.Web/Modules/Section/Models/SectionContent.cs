using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentMetaData))]
    public class SectionContent : EditorEntity
    {
        public enum Types
        {
            CallToAction = 1,
            Image = 2,
            Paragraph = 3,
            Title = 4
        }
        public int ID { get; set; }
        public string SectionWidgetId { get; set; }
        public int SectionGroupId { get; set; }
        public int SectionContentId { get; set; }
        public int SectionContentType { get; set; }
        public int Order { get; set; }

        public SectionContent InitContent(SectionContent content)
        {
            content.SectionWidgetId = SectionWidgetId;
            content.SectionGroupId = SectionGroupId;
            content.SectionContentId = SectionContentId;
            content.SectionContentType = SectionContentType;
            content.Order = Order;
            return content;
        }
    }

    class SectionContentMetaData : DataViewMetaData<SectionContent>
    {

        protected override void DataConfigure()
        {
            DataTable("SectionContent");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
        }

        protected override void ViewConfigure()
        {

        }
    }
}