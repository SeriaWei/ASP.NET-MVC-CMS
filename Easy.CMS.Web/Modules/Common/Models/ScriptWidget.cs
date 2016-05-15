using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(ScriptWidgetMetaData))]
    public class ScriptWidget : WidgetBase
    {
        public string Script { get; set; }
    }

    class ScriptWidgetMetaData : WidgetMetaData<ScriptWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Title).AsHidden();
            ViewConfig(m => m.StyleClass).AsHidden();
            ViewConfig(m => m.Script).AsTextArea().Order(NextOrder()).Required();
        }
    }
}