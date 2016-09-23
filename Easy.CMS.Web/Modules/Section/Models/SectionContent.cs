/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Section.Models
{
    [Serializable]
    public abstract class SectionContentBase : EditorEntity
    {
        [Serializable]
        public enum Types
        {
            None = 0,
            CallToAction = 1,
            Image = 2,
            Paragraph = 3,
            Title = 4,
            Video = 5
        }
        public int? ID { get; set; }
        public string SectionWidgetId { get; set; }
        public int? SectionGroupId { get; set; }
        public int? Order { get; set; }
        public abstract int SectionContentType
        {
            get;
            set;
        }
    }
    [DataConfigure(typeof(SectionContentMetaData)), Serializable]
    public class SectionContent : SectionContentBase
    {
        public override int SectionContentType
        {
            get;
            set;
        }

        public SectionContent InitContent(SectionContent content)
        {
            content.SectionWidgetId = SectionWidgetId;
            content.SectionGroupId = SectionGroupId;
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
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {

        }
    }
}