using System;
using Easy.MetaData;
using Easy.Web.CMS;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(ImageWidgetMedaData)), Serializable]
    public class ImageWidget : WidgetBase
    {
        public string ImageUrl { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Link { get; set; }
        public string AltText { get; set; }
    }
    class ImageWidgetMedaData : WidgetMetaData<ImageWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ImageUrl).AsTextBox().Required().Order(NextOrder()).AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia); 
        }
    }
}