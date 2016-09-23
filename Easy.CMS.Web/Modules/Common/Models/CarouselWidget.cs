/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.CMS.Common.Service;
using Easy.Extend;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(CarouselWidgetMetaData)), Serializable]
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
            ViewConfig(m => m.CarouselID).AsDropDownList().Order(NextOrder()).DataSource(() =>
            {
                var result = new Dictionary<string, string> {{"","---请选择---"}};
                ServiceLocator.Current.GetInstance<ICarouselService>().Get().Each(m => result.Add(m.ID.ToString(), m.Title));
                return result;
            });
            ViewConfig(m => m.CarouselItems).AsListEditor().Order(NextOrder());
        }
    }

}