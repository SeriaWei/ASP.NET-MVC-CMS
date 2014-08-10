using Easy.CMS.Common.Models;
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
                item.ParentId = "0";
            }
            item.ID = Guid.NewGuid().ToString("N");
            base.Add(item);
        }
        public override int Delete(Data.DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any() && this.Get(new Data.DataFilter().Where("ParentId", Constant.OperatorType.In, deletes)).Any())
            {
                this.Delete(new Data.DataFilter().Where("ParentId", Constant.OperatorType.In, deletes));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            var entity = Get(primaryKeys);
            this.Delete(new Data.DataFilter().Where("ParentId", Constant.OperatorType.Equal, entity.ID));
            return base.Delete(primaryKeys);
        }
    }
}