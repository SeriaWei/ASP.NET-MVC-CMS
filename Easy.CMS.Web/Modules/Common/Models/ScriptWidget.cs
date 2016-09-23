/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(ScriptWidgetMetaData)), Serializable]
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