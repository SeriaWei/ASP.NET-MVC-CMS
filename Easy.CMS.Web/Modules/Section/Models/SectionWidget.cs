/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionWidgetMetaData)), Serializable]
    public class SectionWidget : WidgetBase
    {
        public string SectionTitle { get; set; }
        public IEnumerable<SectionGroup> Groups { get; set; }
        public SectionTemplate Template { get; set; }
    }

    class SectionWidgetMetaData : WidgetMetaData<SectionWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.SectionTitle).AsHidden();
            ViewConfig(m => m.Template).AsHidden().Ignore();
        }
    }
}