using Easy.Web.CMS.Widget;
using Easy.Web.CMS.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Common.ViewModels
{
    public class LayoutZonesViewModel
    {
        public string PageID { get; set; }
        public IEnumerable<ZoneEntity> Zones { get; set; }
        public IEnumerable<WidgetBase> Widgets { get; set; }

    }
}