using Easy.CMS.Common.Models;
using Easy.Data;
using Easy.RepositoryPattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Extend;

namespace Easy.CMS.Common.Service
{
    public class NavigationService : ServiceBase<NavigationEntity>
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
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any() && this.Get(new Data.DataFilter().Where("ParentId", OperatorType.In, deletes)).Any())
            {
                this.Delete(new Data.DataFilter().Where("ParentId", OperatorType.In, deletes));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            var entity = Get(primaryKeys);
            this.Delete(new Data.DataFilter().Where("ParentId", OperatorType.Equal, entity.ID));
            return base.Delete(primaryKeys);
        }

        public void Move(string id, string parentId, int position, int oldPosition)
        {
            var nav = this.Get(id);
            nav.ParentId = parentId;
            nav.DisplayOrder = position;
            var filter = new Data.DataFilter()
                .Where("ParentId", OperatorType.Equal, nav.ParentId)
                .Where("Id", OperatorType.NotEqual, nav.ID).OrderBy("DisplayOrder", OrderType.Ascending);
            var navs = this.Get(filter);
            int order = 1;
            for (int i = 0; i < navs.Count(); i++)
            {
                var eleNav = navs.ElementAt(i);
                if (i == position - 1)
                {
                    order++;
                }
                eleNav.DisplayOrder = order;
                this.Update(eleNav);
                order++;
            }
            this.Update(nav);
        }
    }
}