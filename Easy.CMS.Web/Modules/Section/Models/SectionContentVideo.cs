using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentVideoMetaData)), Serializable]
    public class SectionContentVideo : SectionContent
    {
        public string VideoTitle { get; set; }
        public string Thumbnail { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }

        public override int SectionContentType
        {
            get
            {
                return (int)Types.Video;
            }
        }
    }

    class SectionContentVideoMetaData : DataViewMetaData<SectionContentVideo>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionContentVideo");
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.SectionWidgetId).Insert(true).Update(false);
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.VideoTitle).AsTextBox().Required();
            ViewConfig(m => m.Thumbnail).AsTextBox().Required().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.Url).AsTextBox().AddClass(StringKeys.SelectVideoClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.Code).AsTextArea().MaxLength(500);
        }
    }
}