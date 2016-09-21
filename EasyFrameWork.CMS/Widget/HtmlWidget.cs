using System;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;

namespace Easy.Web.CMS.Widget
{
    [DataConfigure(typeof(HtmlWidgetMetaData)), Serializable]
    public class HtmlWidget : WidgetBase
    {
        public string HTML { get; set; }
    }
    class HtmlWidgetMetaData : WidgetMetaData<HtmlWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.HTML).AsTextArea().AddClass("html").Order(NextOrder());
        }
    }

}
