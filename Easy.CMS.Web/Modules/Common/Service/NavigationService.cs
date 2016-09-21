using System;
using System.Linq;
using Easy.CMS.Common.Models;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;

namespace Easy.CMS.Common.Service
{
    public class NavigationService : ServiceBase<NavigationEntity>, INavigationService
    {
        public override void Add(NavigationEntity item)
        {
            if (item.ParentId.IsNullOrEmpty())
            {
                item.ParentId = "#";
            }
            item.ID = Guid.NewGuid().ToString("N");
            base.Add(item);
        }
        public override int Delete(DataFilter filter)
        {
            var deletes = Get(filter).ToList(m => m.ID);
            if (deletes.Any() && Get(new DataFilter().Where("ParentId", OperatorType.In, deletes)).Any())
            {
                Delete(new DataFilter().Where("ParentId", OperatorType.In, deletes));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            var entity = Get(primaryKeys);
            Delete(new DataFilter().Where("ParentId", OperatorType.Equal, entity.ID));
            return base.Delete(primaryKeys);
        }

        public void Move(string id, string parentId, int position, int oldPosition)
        {
            var nav = Get(id);
            nav.ParentId = parentId;
            nav.DisplayOrder = position;
            var filter = new DataFilter()
                .Where("ParentId", OperatorType.Equal, nav.ParentId)
                .Where("Id", OperatorType.NotEqual, nav.ID).OrderBy("DisplayOrder", OrderType.Ascending);
            var navs = Get(filter);
            int order = 1;
            for (int i = 0; i < navs.Count(); i++)
            {
                var eleNav = navs.ElementAt(i);
                if (i == position - 1)
                {
                    order++;
                }
                eleNav.DisplayOrder = order;
                Update(eleNav);
                order++;
            }
            Update(nav);
        }
    }
}