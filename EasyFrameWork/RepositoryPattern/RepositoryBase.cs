/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using Easy.Constant;
using Easy.Data.DataBase;
using Easy.Data;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;

namespace Easy.RepositoryPattern
{
    public class RepositoryBase<T> : IRepository<T> where T : class
    {
        private readonly DataConfigureAttribute _dataConfigure;
        private readonly Type _iEnumerableType;
        private IApplicationContext _applicationContext;
        public IApplicationContext ApplicationContext
        {
            get
            {
                return _applicationContext ??
                       (_applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>());
            }
            set { _applicationContext = value; }
        }
        private DataBasic _dataBase;
        public DataBasic DataBase
        {
            get
            {
                if (_dataBase == null)
                {
                    string dataBaseKey = ConfigurationManager.AppSettings[DataBasic.DataBaseAppSetingKey];
                    _dataBase = ServiceLocator.Current.GetAllInstances<DataBasic>().FirstOrDefault(m => m.DataBaseTypeNames().Any(n => n == dataBaseKey)) ?? new Sql();
                }
                return _dataBase;
            }
            set { _dataBase = value; }
        }
        public RepositoryBase()
        {
            _dataConfigure = DataConfigureAttribute.GetAttribute<T>();
            _iEnumerableType = typeof(IEnumerable);
        }

        public virtual T Get(params object[] primaryKeys)
        {
            return GetReference(DataBase.Get<T>(primaryKeys));
        }
        public virtual IEnumerable<T> Get(DataFilter filter)
        {
            var result = DataBase.Get<T>(filter);
            result.Each(m => GetReference(m));
            return result;
        }
        public virtual IEnumerable<T> Get(DataFilter filter, Pagination pagin)
        {
            var result = DataBase.Get<T>(filter, pagin);
            result.Each(m => GetReference(m));
            return result;
        }
        public virtual void Add(T item)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.CreateBy))
                        entity.CreateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.CreatebyName))
                        entity.CreatebyName = ApplicationContext.CurrentUser.UserName;
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.UserName;
                }
                entity.CreateDate = DateTime.Now;
                entity.LastUpdateDate = DateTime.Now;
            }
            DataBase.Add(item);
            SaveReference(item, ActionType.Create);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            var item = Get(primaryKeys);
            if (item != null)
            {
                SaveReference(item, ActionType.Delete);
            }
            return DataBase.Delete<T>(primaryKeys);
        }
        public virtual int Delete(T item)
        {
            var primaryKey = DataBase.GetPrimaryKeys(_dataConfigure);
            var filter = new DataFilter();
            foreach (PrimaryKey key in primaryKey)
            {
                filter.Where(key.ColumnName, OperatorType.Equal, Reflection.ClassAction.GetPropertyValue(item, key.PropertyName));
            }
            return Delete(filter);
        }
        public virtual int Delete(DataFilter filter)
        {
            Get(filter).Each(m => { SaveReference(m, ActionType.Delete); });
            return DataBase.Delete<T>(filter);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.UserName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            SaveReference(item, ActionType.Update);
            return DataBase.Update(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            if (item is EditorEntity)
            {
                EditorEntity entity = item as EditorEntity;
                if (ApplicationContext != null && ApplicationContext.CurrentUser != null)
                {
                    if (string.IsNullOrEmpty(entity.LastUpdateBy))
                        entity.LastUpdateBy = ApplicationContext.CurrentUser.UserID;
                    if (string.IsNullOrEmpty(entity.LastUpdateByName))
                        entity.LastUpdateByName = ApplicationContext.CurrentUser.UserName;
                }
                entity.LastUpdateDate = DateTime.Now;
            }
            SaveReference(item, ActionType.Update);
            return DataBase.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return DataBase.Count<T>(filter);
        }

        private void SaveReference(T item, ActionType actionType)
        {
            if (item == null)
                return;
            Action<PropertyDataInfo, T, object, ActionType> opeartorChoose = (propertyDataInfo, entity, childEntity, action) =>
            {
                switch (action)
                {
                    case ActionType.Create:
                        {
                            if (!(childEntity is EditorEntity) || ((EditorEntity)childEntity).ActionType == ActionType.Create)
                            {
                                propertyDataInfo.AddReference(entity, childEntity);
                            }
                            break;
                        }
                    case ActionType.Update:
                        {
                            if (childEntity is EditorEntity)
                            {
                                var editor = (EditorEntity)childEntity;
                                if (editor.ActionType == ActionType.Create)
                                {
                                    propertyDataInfo.AddReference(entity, childEntity);
                                }
                                else if (editor.ActionType == ActionType.Update)
                                {
                                    propertyDataInfo.UpdateReference(childEntity);
                                }
                                else if (editor.ActionType == ActionType.Delete)
                                {
                                    propertyDataInfo.DeleteReference(childEntity);
                                }
                            }
                            else
                            {
                                propertyDataInfo.UpdateReference(childEntity);
                            }
                            break;
                        }
                    case ActionType.Delete:
                        {
                            propertyDataInfo.DeleteReference(childEntity);
                            break;
                        }
                }
            };
            _dataConfigure.MetaData.Properties.Each(m =>
            {
                if (_dataConfigure.MetaData.PropertyDataConfig.ContainsKey(m.Key))
                {
                    var dataInfo = _dataConfigure.MetaData.PropertyDataConfig[m.Key];
                    if (dataInfo.IsReference)
                    {
                        var value = m.Value.GetValue(item, null);
                        if (value != null)
                        {
                            if (value is IEnumerable)
                            {
                                foreach (var valueItem in value as IEnumerable)
                                {
                                    opeartorChoose(dataInfo, item, valueItem, actionType);
                                }
                            }
                            else
                            {
                                opeartorChoose(dataInfo, item, value, actionType);
                            }
                        }
                    }
                }
            });
        }

        private T GetReference(T item)
        {
            if (item == null)
                return null;

            _dataConfigure.MetaData.Properties.Each(m =>
            {
                if (_dataConfigure.MetaData.PropertyDataConfig.ContainsKey(m.Key))
                {
                    var dataInfo = _dataConfigure.MetaData.PropertyDataConfig[m.Key];
                    if (dataInfo.IsReference)
                    {
                        var value = dataInfo.GetReference(item);
                        if (_iEnumerableType.IsAssignableFrom(m.Value.PropertyType))
                        {
                            m.Value.SetValue(item, value, null);
                        }
                        else
                        {
                            foreach (var valueItem in value)
                            {
                                m.Value.SetValue(item, valueItem, null);
                                break;
                            }
                        }
                    }
                }
            });
            return item;
        }
    }
}
