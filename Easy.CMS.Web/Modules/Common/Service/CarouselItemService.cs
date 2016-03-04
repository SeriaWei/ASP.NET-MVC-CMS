using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.RepositoryPattern;
using Easy.CMS.Common.Models;
using Easy.Data;

namespace Easy.CMS.Common.Service
{
    public class CarouselItemService : ServiceBase<CarouselItemEntity>, ICarouselItemService
    {
        public override void Add(CarouselItemEntity item)
        {
            if (item.ActionType != Constant.ActionType.Unattached)
            {
                base.Add(item);
            }
        }

        public override bool Update(CarouselItemEntity item, DataFilter filter)
        {
            if (item.ActionType == Constant.ActionType.Update)
            {
                base.Update(item, filter);
            }
            else if (item.ActionType == Constant.ActionType.Create)
            {
                base.Add(item);
            }
            else if (item.ActionType == Constant.ActionType.Delete)
            {
                base.Delete(filter);
            }
            return true;
        }

        public override bool Update(CarouselItemEntity item, params object[] primaryKeys)
        {
            if (item.ActionType == Constant.ActionType.Update)
            {
                base.Update(item, primaryKeys);
            }
            else if (item.ActionType == Constant.ActionType.Create)
            {
                base.Add(item);
            }
            else if (item.ActionType == Constant.ActionType.Delete)
            {
                base.Delete(item.ID);
            }
            return true;
        }
    }
}