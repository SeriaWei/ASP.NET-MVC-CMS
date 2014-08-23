using Easy.CMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Common.Service
{
    public class CarouselWidgetService : Widget.WidgetService<CarouselWidget>
    {
        public override Widget.WidgetPart Display(Widget.WidgetBase widget, HttpContextBase httpContext)
        {
            CarouselWidget cWidget = widget as CarouselWidget;
            return cWidget.ToWidgetPart(new CarouselService().Get(cWidget.CarouselID));
        }
    }
}