using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Easy.Data.DataBase;
using Easy.Data;
using Easy.Models;

namespace Easy.RepositoryPattern
{
    public class RepositoryBase<T> : IRepository<T>, IAdapterRepository where T : class
    {
        static string dataBase;
        static string connString;
        static RepositoryBase()
        {
            dataBase = System.Configuration.ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
            var con = System.Configuration.ConfigurationManager.ConnectionStrings[DataBasic.ConnectionKey];
            if (con != null)
            {
                connString = con.ConnectionString;
            }
            else
            {
                connString = System.Configuration.ConfigurationManager.ConnectionStrings[0].ConnectionString;
            }
        }
        public static DataBasic DB
        {
            get;
            private set;
        }
        public RepositoryBase()
        {
            if (dataBase == DataBasic.Ace)
            {
                DB = new Access(connString);
                (DB as Access).DbType = Access.DdTypes.Ace;
            }
            else if (dataBase == DataBasic.Jet)
            {
                DB = new Access(connString);
                (DB as Access).DbType = Access.DdTypes.JET;
            }
            else if (dataBase == DataBasic.SQL)
            {
                DB = new SQL(connString);
            }
            else
            {
                DB = new SQL(connString);
            }
        }

        public virtual T Get(params object[] primaryKeys)
        {
            return DB.Get<T>(primaryKeys);
        }
        public virtual List<T> Get(DataFilter filter)
        {
            return DB.Get<T>(filter);
        }
        public virtual List<T> Get(DataFilter filter, Pagination pagin)
        {
            return DB.Get<T>(filter, pagin);
        }
        public virtual void Add(T item)
        {
            DB.Add<T>(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return DB.Delete<T>(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return DB.Delete<T>(filter);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            return DB.Update<T>(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            return DB.Update<T>(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return DB.Count<T>(filter);
        }
    }
}
