using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using Easy.Web.CMS;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(VideoWidgetMetaData)), Serializable]
    public class VideoWidget : WidgetBase
    {
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Url { get; set; }
        public string Code { get; set; }
    }

    class VideoWidgetMetaData : WidgetMetaData<VideoWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Url).AsTextBox().Order(NextOrder()).AddClass(StringKeys.SelectVideoClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.Code).AsTextArea().Order(NextOrder()).MaxLength(500);
        }
    }
}