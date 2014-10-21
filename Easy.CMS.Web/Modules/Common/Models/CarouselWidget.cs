using Easy.CMS.MetaData;
using Easy.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.CMS.Common.Service;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(CarouselWidgetMetaData))]
    public class CarouselWidget : WidgetBase
    {
        public long CarouselID { get; set; }
    }
    class CarouselWidgetMetaData : WidgetMetaData<CarouselWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.CarouselID).AsDropDownList().DataSource(new CarouselService().Get().ToDictionary(m => m.ID.ToString(), m => m.Title));
        }
    }

}