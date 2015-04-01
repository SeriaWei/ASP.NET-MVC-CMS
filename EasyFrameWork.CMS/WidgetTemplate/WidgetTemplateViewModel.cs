using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.WidgetTemplate
{
    public class WidgetTemplateViewModel
    {
        public string PageID { get; set; }
        public string LayoutID { get; set; }
        public string ZoneID { get; set; }
        public string ReturnUrl { get; set; }
        public List<WidgetTemplateEntity> WidgetTemplates { get; set; }
    }
}
