using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.CMS.Common.Service;
using Easy.Extend;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(CarouselWidgetMetaData))]
    public class CarouselWidget : WidgetBase
    {
        public long? CarouselID { get; set; }
        public IEnumerable<CarouselItemEntity> CarouselItems { get; set; } 
    }
    class CarouselWidgetMetaData : WidgetMetaData<CarouselWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.CarouselID).AsDropDownList().DataSource(() =>
            {
                var result = new Dictionary<string, string> {{"","---请选择---"}};
                new CarouselService().Get().Each(m => result.Add(m.ID.ToString(), m.Title));
                return result;
            });
            ViewConfig(m => m.CarouselItems).AsCollectionArea();
        }
    }

}