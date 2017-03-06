/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Data;
using System.Collections.Generic;
using Easy.Data.DataBase;
using Easy.IOC;

namespace Easy.RepositoryPattern
{
    public interface IRepository<T> : IDependency where T : class
    {
        DataBasic DataBase { get; set; }
        IApplicationContext ApplicationContext { get; set; }
        void Add(T item);
        int Delete(DataFilter filter);
        int Delete(params object[] primaryKeys);
        int Delete(T item);IEnumerable<T> Get(DataFilter filter);
        IEnumerable<T> Get(DataFilter filter, Pagination pagin);
        T Get(params object[] primaryKeys);
        bool Update(T item, DataFilter filter);
        bool Update(T item, params object[] primaryKeys);
        long Count(DataFilter filter);
    }
}
