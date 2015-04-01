using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.Widget
{
    public class HtmlWidgetService : WidgetService<HtmlWidget>
    {
        public override WidgetPart Display(WidgetBase widget, System.Web.HttpContextBase httpContext)
        {
            return base.Display(widget, httpContext);
        }

        
    }
}
