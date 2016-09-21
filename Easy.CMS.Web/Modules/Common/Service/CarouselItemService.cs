using Easy.CMS.Common.Models;
using Easy.Constant;
using Easy.Data;
using Easy.RepositoryPattern;

namespace Easy.CMS.Common.Service
{
    public class CarouselItemService : ServiceBase<CarouselItemEntity>, ICarouselItemService
    {
        public override void Add(CarouselItemEntity item)
        {
            if (item.ActionType != ActionType.Unattached)
            {
                base.Add(item);
            }
        }

        public override bool Update(CarouselItemEntity item, DataFilter filter)
        {
            if (item.ActionType == ActionType.Update)
            {
                base.Update(item, filter);
            }
            else if (item.ActionType == ActionType.Create)
            {
                base.Add(item);
            }
            else if (item.ActionType == ActionType.Delete)
            {
                Delete(filter);
            }
            return true;
        }

        public override bool Update(CarouselItemEntity item, params object[] primaryKeys)
        {
            if (item.ActionType == ActionType.Update)
            {
                base.Update(item, primaryKeys);
            }
            else if (item.ActionType == ActionType.Create)
            {
                base.Add(item);
            }
            else if (item.ActionType == ActionType.Delete)
            {
                Delete(item.ID);
            }
            return true;
        }
    }
}