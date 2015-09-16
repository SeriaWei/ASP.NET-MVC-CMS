using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.CMS.Common.Models;
using Easy.Extend;

namespace Easy.CMS.Common.Service
{
    public class CarouselService : ServiceBase<CarouselEntity>
    {
        public override void Add(CarouselEntity item)
        {
            base.Add(item);
            if (item.CarouselItems != null)
            {
                var carouselItemService = new CarouselItemService();
                item.CarouselItems.Each(m =>
                {
                    m.CarouselID = item.ID;
                    carouselItemService.Add(m);
                });
            }
        }
        public override bool Update(CarouselEntity item, params object[] primaryKeys)
        {
            bool result = base.Update(item, primaryKeys);
            if (item.CarouselItems != null)
            {
                var carouselItemService = new CarouselItemService();
                item.CarouselItems.Each(m =>
                {
                    m.CarouselID = item.ID;
                    carouselItemService.Update(m);
                });
            }
            return result;
        }
        public override CarouselEntity Get(params object[] primaryKeys)
        {
            CarouselEntity entity = base.Get(primaryKeys);
            var carouselItemService = new CarouselItemService();
            entity.CarouselItems = carouselItemService.Get("CarouselID", OperatorType.Equal, entity.ID);
            entity.CarouselItems.Each(m => m.ActionType = Constant.ActionType.Update);
            return entity;
        }
    }
}