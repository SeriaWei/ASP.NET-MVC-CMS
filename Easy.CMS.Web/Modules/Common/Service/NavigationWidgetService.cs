using Easy.CMS.Common.Models;
using Easy.Data;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Common.Service
{
    public class NavigationWidgetService : WidgetService<NavigationWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var navs = new NavigationService().Get(new Data.DataFilter().OrderBy("DisplayOrder", OrderType.Ascending));
            return widget.ToWidgetPart(navs);
        }
    }
}