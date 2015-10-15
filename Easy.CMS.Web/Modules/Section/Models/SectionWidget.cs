using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionWidgetMetaData))]
    public class SectionWidget : WidgetBase
    {
        public string SectionTitle { get; set; }
        public bool IsHorizontal { get; set; }
        public IEnumerable<SectionGroup> Groups { get; set; }
    }

    class SectionWidgetMetaData : WidgetMetaData<SectionWidget>
    {

    }
}