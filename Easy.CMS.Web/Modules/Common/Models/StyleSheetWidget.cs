using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(StyleSheetWidgetMetaData)), Serializable]
    public class StyleSheetWidget : WidgetBase
    {
        public string StyleSheet { get; set; }
    }

    class StyleSheetWidgetMetaData : WidgetMetaData<StyleSheetWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.StyleClass).AsHidden();
            ViewConfig(m => m.StyleSheet).AsTextArea().Order(NextOrder()).Required();
        }
    }
}