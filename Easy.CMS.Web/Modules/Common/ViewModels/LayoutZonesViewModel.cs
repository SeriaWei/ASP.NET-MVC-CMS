using Easy.Web.CMS.Layout;
using Easy.Web.CMS.Widget;
using Easy.Web.CMS.Zone;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS.Page;

namespace Easy.CMS.Common.ViewModels
{
    public class LayoutZonesViewModel
    {
        public LayoutEntity Layout { get; set; }
        public PageEntity Page { get; set; }
        public string PageID { get; set; }
        public string LayoutID { get; set; }
        public IEnumerable<ZoneEntity> Zones { get; set; }
        public IEnumerable<WidgetBase> Widgets { get; set; }
        public LayoutHtmlCollection LayoutHtml { get; set; }

    }
}