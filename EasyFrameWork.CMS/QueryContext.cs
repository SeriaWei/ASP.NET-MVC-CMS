using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS
{
    public class QueryContext
    {
        public string LayoutID { get; set; }
        public string ZoneID { get; set; }
        public string PageID { get; set; }
        public string WidgetID { get; set; }
        public string ReturnUrl { get; set; }
        public long WidgetTemplateID { get; set; }
    }
}
