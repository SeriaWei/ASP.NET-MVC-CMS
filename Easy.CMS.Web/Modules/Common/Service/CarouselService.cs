using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
                CarouselItemService carouselItemService = new CarouselItemService();
                item.CarouselItems.Each(m =>
                {
                    if (m.ActionType != Constant.ActionType.Unattached)
                    {
                        m.CarouselID = item.ID;
                        carouselItemService.Add(m);
                    }
                });
            }
        }
        public override bool Update(CarouselEntity item, params object[] primaryKeys)
        {
            bool result = base.Update(item, primaryKeys);
            if (item.CarouselItems != null)
            {
                CarouselItemService carouselItemService = new CarouselItemService();
                item.CarouselItems.Each(m =>
                {
                    if (m.ActionType == Constant.ActionType.Update)
                    {
                        m.CarouselID = item.ID;
                        carouselItemService.Update(m);
                    }
                    else if (m.ActionType == Constant.ActionType.Create)
                    {
                        m.CarouselID = item.ID;
                        carouselItemService.Add(m);
                    }
                    else if (m.ActionType == Constant.ActionType.Delete)
                    {
                        carouselItemService.Delete(m.ID);
                    }
                });
            }
            return result;
        }
        public override CarouselEntity Get(params object[] primaryKeys)
        {
            CarouselEntity entity = base.Get(primaryKeys);
            CarouselItemService carouselItemService = new CarouselItemService();
            entity.CarouselItems = carouselItemService.Get("CarouselID", Constant.OperatorType.Equal, entity.ID);
            entity.CarouselItems.Each(m => m.ActionType = Constant.ActionType.Update);
            return entity;
        }
    }
}