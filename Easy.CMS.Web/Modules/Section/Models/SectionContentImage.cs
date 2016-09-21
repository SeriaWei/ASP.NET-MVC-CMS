using System;
using Easy.MetaData;
using Easy.Web.CMS;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentImageMetaData)), Serializable]
    public class SectionContentImage : SectionContent
    {
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
        public string ImageTitle { get; set; }
        public string Href { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public override int SectionContentType
        {
            get
            {
                return (int)Types.Image;
            }
        }
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
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.SectionWidgetId).Insert(true).Update(false);
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ImageSrc).AsTextBox().Required().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.Href).AsTextBox().AddClass(StringKeys.SelectPageClass).AddProperty("data-url", Urls.SelectPage);
        }
    }
}