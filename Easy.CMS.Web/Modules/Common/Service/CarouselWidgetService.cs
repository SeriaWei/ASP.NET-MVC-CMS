using Easy.CMS.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Common.Service
{
    public class CarouselWidgetService : WidgetService<CarouselWidget>
    {
        private readonly ICarouselItemService _carouselItemService;

        public CarouselWidgetService()
        {
            _carouselItemService = ServiceLocator.Current.GetInstance<ICarouselItemService>();
        }

        public override WidgetBase GetWidget(WidgetBase widget)
        {
            var carouselWidget = base.GetWidget(widget) as CarouselWidget;

            carouselWidget.CarouselItems = _carouselItemService.Get("CarouselWidgetID", OperatorType.Equal,
                    carouselWidget.ID);
            carouselWidget.CarouselItems.Each(m => m.ActionType = Constant.ActionType.Update);
            return carouselWidget;
        }

        public override void Add(CarouselWidget item)
        {
            base.Add(item);
            if (item.CarouselItems != null && item.CarouselItems.Any())
            {
                item.CarouselItems.Each(m =>
                {
                    m.CarouselWidgetID = item.ID;
                    _carouselItemService.Add(m);
                });
            }
        }

        public override bool Update(CarouselWidget item, params object[] primaryKeys)
        {
            if (item.CarouselItems != null && item.CarouselItems.Any())
            {
                item.CarouselItems.Each(m =>
                {
                    m.CarouselWidgetID = item.ID;
                    _carouselItemService.Update(m);
                });
            }
            return base.Update(item, primaryKeys);
        }

        public override void DeleteWidget(string widgetId)
        {
            _carouselItemService.Delete(new DataFilter().Where("CarouselWidgetID", OperatorType.Equal, widgetId));
            base.DeleteWidget(widgetId);
        }

        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            var carouselWidget = widget as CarouselWidget;
            if (carouselWidget.CarouselID.HasValue)
            {
                var varouselItems = _carouselItemService.Get("CarouselID", OperatorType.Equal,
                        carouselWidget.CarouselID);
                if (carouselWidget.CarouselItems == null)
                {
                    carouselWidget.CarouselItems = varouselItems;
                }
                else
                {
                    ((List<CarouselItemEntity>)carouselWidget.CarouselItems).AddRange(varouselItems);
                }
            }
            carouselWidget.CarouselItems =
                carouselWidget.CarouselItems.Where(m => m.Status == (int)Constant.RecordStatus.Active);
            return base.Display(widget, httpContext);
        }
    }
}