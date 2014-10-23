using Easy.CMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.Service
{
    public class CarouselWidgetService : WidgetService<CarouselWidget>
    {
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var cWidget = widget as CarouselWidget;
            return cWidget.ToWidgetPart(new CarouselService().Get(cWidget.CarouselID));
        }
    }
}