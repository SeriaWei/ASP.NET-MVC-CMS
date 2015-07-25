using Easy.Data;
using System;
using System.Collections.Generic;
namespace Easy.RepositoryPattern
{
    public interface IService
    {
        void AddGeneric<T>(T item) where T : class;
        IEnumerable<T> GetGeneric<T>() where T : class;
        IEnumerable<T> GetGeneric<T>(DataFilter filter) where T : class;
        IEnumerable<T> GetGeneric<T>(DataFilter filter, Pagination pagin) where T : class;
        T GetGeneric<T>(params object[] primaryKeys) where T : class;
        bool UpdateGeneric<T>(T item, DataFilter filter) where T : class;
        bool UpdateGeneric<T>(T item, params object[] primaryKeys) where T : class;
        long Count(DataFilter filter);
        int Delete(DataFilter filter);
        int Delete(params object[] primaryKeys);
    }
    
}
