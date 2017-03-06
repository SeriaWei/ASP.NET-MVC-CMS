/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Models;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Practices.ServiceLocation;
using System.Transactions;

namespace Easy.RepositoryPattern
{
    public abstract class ServiceBase<T> : IService<T> where T : class
    {
        private IRepository<T> _repository;

        public IRepository<T> Repository
        {
            get { return _repository ?? (_repository = new RepositoryBase<T>()); }
            set { _repository = value; }
        }

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

        [DebuggerStepThrough]
        protected TResult ExecuteTransaction<TResult>(Func<TResult> command)
        {
            var options = new TransactionOptions { IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted };
            using (var ts = new TransactionScope(TransactionScopeOption.Required, options))
            {
                try
                {
                    var result = command.Invoke();
                    ts.Complete();
                    return result;
                }
                catch (Exception exception)
                {
                    Logger.Error(exception);
                    return default(TResult);
                }
            }
        }
        public virtual T Get(params object[] primaryKeys)
        {
            return Repository.Get(primaryKeys);
        }
        public virtual IEnumerable<T> Get()
        {
            return Repository.Get(new DataFilter());
        }

        public virtual IEnumerable<T> Get(Expression<Func<T, bool>> expression)
        {
            return Get(Reflection.LinqExpression.ConvertToDataFilter(expression.Parameters, (BinaryExpression)expression.Body));
        }
        public virtual IEnumerable<T> Get(DataFilter filter)
        {
            return Repository.Get(filter);
        }
        public virtual IEnumerable<T> Get(DataFilter filter, Pagination pagin)
        {
            return Repository.Get(filter, pagin);
        }
        public virtual IEnumerable<T> Get(string property, OperatorType operatorType, object value)
        {
            return Repository.Get(new DataFilter().Where(property, operatorType, value));
        }
        public virtual void Add(T item)
        {
            Repository.Add(item);
        }
        public virtual int Delete(params object[] primaryKeys)
        {
            return Repository.Delete(primaryKeys);
        }
        public virtual int Delete(DataFilter filter)
        {
            return Repository.Delete(filter);
        }
        public virtual int Delete(Expression<Func<T, bool>> expression)
        {
            return Delete(Reflection.LinqExpression.ConvertToDataFilter(expression.Parameters, expression.Body as BinaryExpression));
        }
        public virtual int Delete(T item)
        {
            return Repository.Delete(item);
        }
        public virtual bool Update(T item, DataFilter filter)
        {
            return Repository.Update(item, filter);
        }
        public virtual bool Update(T item, params object[] primaryKeys)
        {
            return Repository.Update(item, primaryKeys);
        }
        public virtual long Count(DataFilter filter)
        {
            return Repository.Count(filter);
        }

        public virtual long Count(Expression<Func<T, bool>> expression)
        {
            return Count(Reflection.LinqExpression.ConvertToDataFilter(expression.Parameters, expression.Body as BinaryExpression));
        }
    }
}
