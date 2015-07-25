using Easy.Models;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.RepositoryPattern
{
    public abstract class ServiceBase<Entity> : IService, IAdapterService, IServiceBase<Entity> where Entity : class
    {
        private readonly RepositoryBase<Entity> repBase;
        private readonly IApplicationContext applicationContext;
        public ServiceBase()
        {
            repBase = new RepositoryBase<Entity>();
            applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>();
        }
        public virtual Entity Get(params object[] primaryKeys)
        {
            return repBase.Get(primaryKeys);
        }
        public virtual IEnumerable<Entity> Get()
        {
            return repBase.Get(new DataFilter());
        }
        public virtual IEnumerable<Entity> Get(DataFilter filter)
        {
            return repBase.Get(filter);
        }
        public virtual IEnumerable<Entity> Get(DataFilter filter, Pagination pagin)
        {
            return repBase.Get(filter, pagin);
        }
        public virtual IEnumerable<Entity> Get(string property, OperatorType operatorType, object value)
        {
            return repBase.Get(new DataFilter().Where(property, operatorType, value));
        }
        public virtual void Add(Entity item)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (applicationContext != null && applicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.CreateBy))
                        entity.CreateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.CreatebyName))
                        entity.CreatebyName = applicationContext.CurrentUser.NickName;
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = applicationContext.CurrentUser.NickName;
                }
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
            }
            repBase.Add(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return repBase.Delete(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return repBase.Delete(filter);
        }
        public virtual bool Update(Entity item, DataFilter filter)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (applicationContext != null && applicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = applicationContext.CurrentUser.NickName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            return repBase.Update(item, filter);
        }
        public virtual bool Update(Entity item, params object[] primaryKeys)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (applicationContext != null && applicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = applicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = applicationContext.CurrentUser.NickName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            return repBase.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return repBase.Count(filter);
        }

        public virtual void AddGeneric<T>(T item) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            rep.Add(item);
        }

        public virtual IEnumerable<T> GetGeneric<T>() where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(new DataFilter());
        }

        public virtual IEnumerable<T> GetGeneric<T>(DataFilter filter) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(filter);
        }

        public virtual IEnumerable<T> GetGeneric<T>(DataFilter filter, Pagination pagin) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(filter, pagin);
        }

        public virtual T GetGeneric<T>(params object[] primaryKeys) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Get(primaryKeys);
        }

        public virtual bool UpdateGeneric<T>(T item, DataFilter filter) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Update(item, filter);
        }

        public virtual bool UpdateGeneric<T>(T item, params object[] primaryKeys) where T : class
        {
            RepositoryBase<T> rep = new RepositoryBase<T>();
            return rep.Update(item, primaryKeys);
        }



    }
}
