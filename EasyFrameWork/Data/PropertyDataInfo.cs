/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Data;
using Easy.MetaData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Easy.Data.ValueProvider;
using Easy.RepositoryPattern;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Data
{
    public class PropertyDataInfo
    {
        public PropertyDataInfo(string propertyName)
        {
            this.Ignore = false;
            this.CanInsert = true;
            this.CanUpdate = true;
            this.IsPrimaryKey = false;
            this.IsIncreasePrimaryKey = false;
            this.PropertyName = propertyName;
            this.ColumnName = PropertyName;
            this.StringLength = 255;
        }
        /// <summary>
        /// 是否是主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }
        /// <summary>
        /// 是否为自增主键
        /// </summary>
        public bool IsIncreasePrimaryKey { get; set; }
        public IValueProvider ValueProvider { get; set; }
        public int PrimaryKeyIndex { get; set; }
        /// <summary>
        /// 增改查是忽略
        /// </summary>
        public bool Ignore { get; set; }
        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool CanUpdate { get; set; }
        /// <summary>
        /// 是否可更新
        /// </summary>
        public bool CanInsert { get; set; }
        /// <summary>
        /// 属性名称
        /// </summary>
        public string PropertyName { get; set; }
        /// <summary>
        /// 对应的数据库列名
        /// </summary>
        public string ColumnName { get; set; }
        /// <summary>
        /// 是否是关系字段，即值是否来源其它表
        /// </summary>
        public bool IsRelation { get; set; }
        /// <summary>
        /// 关联表别名
        /// </summary>
        public string TableAlias { get; set; }
        public DbType ColumnType { get; set; }
        public int StringLength { get; set; }
        public bool IsReference { get; set; }
        public Func<object, IEnumerable> GetReference { get; set; }
        public Action<object> UpdateReference { get; set; }
        public Action<object, object> AddReference { get; set; }
        public Action<object> DeleteReference { get; set; }

    }


    public class PropertyDataInfoHelper<T> where T : class
    {
        private readonly PropertyDataInfo _dataConig;
        private readonly IDataViewMetaData _viewMetaData;
        public PropertyDataInfoHelper(PropertyDataInfo item, IDataViewMetaData viewMetaData)
        {
            _dataConig = item;
            _viewMetaData = viewMetaData;
        }
        /// <summary>
        /// 完全忽略，增改查都不管
        /// </summary>
        /// <param name="ignore"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> Ignore(bool? ignore = true)
        {
            bool nore = ignore ?? true;
            _dataConig.Ignore = nore;
            return this;
        }
        /// <summary>
        /// 是否可更新
        /// </summary>
        /// <param name="canUpdate"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> Update(bool canUpdate)
        {
            _dataConig.CanUpdate = canUpdate;
            return this;
        }
        /// <summary>
        /// 是否可插入
        /// </summary>
        /// <param name="canInsert"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> Insert(bool canInsert)
        {
            _dataConig.CanInsert = canInsert;
            return this;
        }
        /// <summary>
        /// 将字段和数据库列进行映射，如果名称不一样的话。
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> Mapper(string column)
        {
            _dataConig.ColumnName = column;
            return this;
        }
        /// <summary>
        /// 关联关系
        /// </summary>
        /// <param name="alias">表别名</param>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> Relation(string alias)
        {
            _dataConig.IsRelation = true;
            _dataConig.TableAlias = alias;
            Insert(false);
            Update(false);
            return this;
        }

        /// <summary>
        /// 设置主键
        /// </summary>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> AsPrimaryKey()
        {
            if (!_dataConig.IsPrimaryKey)
            {
                _dataConig.IsIncreasePrimaryKey = false;
                _dataConig.IsPrimaryKey = true;
                Update(false);
            }
            return this;
        }

        public PropertyDataInfoHelper<T> SetValueProvider(IValueProvider primaryKeyProvider)
        {
            _dataConig.ValueProvider = primaryKeyProvider;
            return this;
        }
        /// <summary>
        /// 设置为自增主键
        /// </summary>
        /// <returns></returns>
        public PropertyDataInfoHelper<T> AsIncreasePrimaryKey()
        {
            if (!_dataConig.IsPrimaryKey)
            {
                _dataConig.IsIncreasePrimaryKey = true;
                _dataConig.IsPrimaryKey = true;
                Insert(false);
                Update(false);
            }
            return this;
        }


        public PropertyDataInfoHelper<T> SetDbType(DbType dbType)
        {
            _dataConig.ColumnType = dbType;
            return this;
        }
        public PropertyDataInfoHelper<T> SetLength(int length)
        {
            _dataConig.StringLength = length;
            return this;
        }

        public PropertyDataInfoHelper<T> SetReference<TEntity, TService>(Expression<Func<T, TEntity, bool>> relation)
            where TEntity : class
            where TService : IService<TEntity>
        {
            Ignore();
            _dataConig.IsReference = true;

            _dataConig.AddReference = (item, childItem) =>
            {
                var _referenceService = ServiceLocator.Current.GetInstance(typeof(TService));
                var service = _referenceService as IService<TEntity>;
                if (service != null)
                {
                    Reflection.LinqExpression.CopyTo(item, childItem, relation.Parameters, relation.Body as BinaryExpression);
                    service.Add((TEntity)childItem);
                }
            };

            _dataConig.DeleteReference = obj =>
            {
                var _referenceService = ServiceLocator.Current.GetInstance(typeof(TService));
                var service = _referenceService as IService<TEntity>;
                if (service != null)
                {
                    service.Delete(obj as TEntity);
                }
            };

            _dataConig.GetReference = obj =>
            {
                var _referenceService = ServiceLocator.Current.GetInstance(typeof(TService));
                var service = _referenceService as IService<TEntity>;
                if (service != null)
                    return service.Get(Reflection.LinqExpression.ConvertToDataFilter(relation.Parameters, relation.Body as BinaryExpression, obj));
                return null;
            };

            _dataConig.UpdateReference = obj =>
            {
                var _referenceService = ServiceLocator.Current.GetInstance(typeof(TService));
                var service = _referenceService as IService<TEntity>;
                if (service != null)
                {
                    service.Update(obj as TEntity);
                }
            };
            return this;
        }

    }
}
