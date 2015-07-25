using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.RepositoryPattern
{
    public interface IServiceBase<Entity>
    {
        Entity Get(params object[] primaryKeys);
        IEnumerable<Entity> Get();
        IEnumerable<Entity> Get(DataFilter filter);
        IEnumerable<Entity> Get(DataFilter filter, Pagination pagin);
        IEnumerable<Entity> Get(string property, OperatorType operatorType, object value);
        void Add(Entity item);
        int Delete(params object[] primaryKeys);
        int Delete(DataFilter filter);
        bool Update(Entity item, DataFilter filter);
        bool Update(Entity item, params object[] primaryKeys);
        long Count(DataFilter filter);
    }
}
