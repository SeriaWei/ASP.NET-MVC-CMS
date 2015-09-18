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
        private readonly CarouselItemService _carouselItemService;

        public CarouselService()
        {
            _carouselItemService = new CarouselItemService();
        }
        public override void Add(CarouselEntity item)
        {
            base.Add(item);
            if (item.CarouselItems != null)
            {
                item.CarouselItems.Each(m =>
                {
                    m.CarouselID = item.ID;
                    _carouselItemService.Add(m);
                });
            }
        }
        public override bool Update(CarouselEntity item, params object[] primaryKeys)
        {
            bool result = base.Update(item, primaryKeys);
            if (item.CarouselItems != null)
            {
                item.CarouselItems.Each(m =>
                {
                    m.CarouselID = item.ID;
                    _carouselItemService.Update(m);
                });
            }
            return result;
        }
        public override CarouselEntity Get(params object[] primaryKeys)
        {
            CarouselEntity entity = base.Get(primaryKeys);
            entity.CarouselItems = _carouselItemService.Get("CarouselID", OperatorType.Equal, entity.ID);
            entity.CarouselItems.Each(m => m.ActionType = Constant.ActionType.Update);
            return entity;
        }

        public override IEnumerable<CarouselEntity> Get(DataFilter filter)
        {
            var carousels= base.Get(filter);
            carousels.Each(m =>
            {
                m.CarouselItems = _carouselItemService.Get("CarouselID", OperatorType.Equal, m.ID);
            });
            return carousels;
        }

        public override int Delete(DataFilter filter)
        {
            this.Get(filter).Each(m => _carouselItemService.Delete(new DataFilter().Where("CarouselID", OperatorType.Equal, m.ID)));
            return base.Delete(filter);
        }

        public override int Delete(params object[] primaryKeys)
        {
            var carousel= Get(primaryKeys);
            if (carousel != null)
            {
                _carouselItemService.Delete(new DataFilter().Where("CarouselID", OperatorType.Equal, carousel.ID));
            }
            return base.Delete(primaryKeys);
        }
    }
}