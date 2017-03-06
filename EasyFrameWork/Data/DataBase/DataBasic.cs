/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Data.Common;
using System.Linq;
using Easy.MetaData;
using Easy.Constant;
using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;
using System.Text;
using Easy.Extend;
using Easy.IOC;

namespace Easy.Data.DataBase
{
    /// <summary>
    /// 数据库基类，AppConfig> DataBase>[SQL,Jet,Ace]
    /// </summary>
    public abstract class DataBasic : IDependency
    {
        public const string DataBaseAppSetingKey = "DataBase";
        public const string ConnectionKey = "Easy";

        #region 私有方法

        protected virtual string GetDBTypeStr(DbType dbType, int length = 255)
        {
            switch (dbType)
            {
                case DbType.Boolean: return "Bit";
                case DbType.Byte:
                    return "TinyInt";
                case DbType.String:
                    return length > 0 ? "nvarchar(" + length + ")" : "text";
                case DbType.DateTime:
                    return "DateTime";
                case DbType.Currency:
                    return "Money";
                case DbType.Double:
                    return "Float";
                case DbType.Decimal:
                    return "decimal(18, 4)";
                case DbType.Int16:
                    return "SmallInt";
                case DbType.Int32:
                case DbType.Int64:
                case DbType.UInt16:
                case DbType.UInt32:
                case DbType.UInt64:
                    return "Int";
                case DbType.SByte:
                    return "TinyInt";
                case DbType.Single:
                    return "Real";
                default: return "nvarchar(" + length + ")";
            }
        }

        protected string GetSelectColumn<T>(DataConfigureAttribute custAttribute, out List<KeyValuePair<string, string>> comMatch) where T : class
        {
            PropertyInfo[] propertys = typeof(T).GetProperties();
            var selectCom = new StringBuilder();
            comMatch = new List<KeyValuePair<string, string>>();
            foreach (var item in propertys)
            {
                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(item.Name))
                    {
                        PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[item.Name];
                        if (!config.Ignore)
                        {
                            if (!string.IsNullOrEmpty(config.ColumnName))
                            {
                                string alias = config.IsRelation ? config.TableAlias : custAttribute.MetaData.Alias;
                                selectCom.AppendFormat("[{0}].[{1}],", alias, config.ColumnName);
                                var keyVale = new KeyValuePair<string, string>(config.ColumnName, item.Name);
                                comMatch.Add(keyVale);
                            }
                            else
                            {
                                string alias = config.IsRelation ? config.TableAlias : custAttribute.MetaData.Alias;
                                selectCom.AppendFormat("[{0}].[{1}],", alias, item.Name);
                                var keyVale = new KeyValuePair<string, string>(item.Name, item.Name);
                                comMatch.Add(keyVale);
                            }
                        }
                    }
                    else
                    {
                        selectCom.AppendFormat("[{0}].[{1}],", custAttribute.MetaData.Alias, item.Name);
                        var keyVale = new KeyValuePair<string, string>(item.Name, item.Name);
                        comMatch.Add(keyVale);
                    }
                }
                else
                {
                    selectCom.AppendFormat("[{0}],", item.Name);
                    var keyVale = new KeyValuePair<string, string>(item.Name, item.Name);
                    comMatch.Add(keyVale);
                }
            }
            return selectCom.ToString().Trim(',');
        }

        protected string GetTableName<T>(DataConfigureAttribute custAttribute) where T : class
        {
            string tableName = null;
            if (custAttribute != null)
            {
                if (!string.IsNullOrEmpty(custAttribute.MetaData.Table))
                {
                    tableName = custAttribute.MetaData.Table;
                }
            }
            if (string.IsNullOrEmpty(tableName))
            {
                tableName = typeof(T).Name;
            }
            return tableName;
        }

        private IEnumerable<string> GetCulumnSchema<T>(DataConfigureAttribute custAttribute) where T : class
        {
            PropertyInfo[] propertys = typeof(T).GetProperties();
            var result = new List<string>();
            foreach (var item in propertys)
            {
                string isNull = "null";
                var code = Type.GetTypeCode(item.PropertyType.IsGenericType ? item.PropertyType.GetGenericArguments()[0] : item.PropertyType);
                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(item.Name))
                    {
                        PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[item.Name];
                        if (config.IsPrimaryKey)
                        {
                            isNull = "not null";
                        }
                        if (config.Ignore || config.IsRelation) continue;

                        if (!string.IsNullOrEmpty(config.ColumnName))
                        {
                            result.Add(string.Format("[{0}] {1} {2},", config.ColumnName, config.IsIncreasePrimaryKey ? "INT IDENTITY" : GetDBTypeStr(Common.ConvertToDbType(code), config.StringLength), isNull));
                        }
                        else
                        {
                            result.Add(string.Format("[{0}] {1} {2},", config.PropertyName, GetDBTypeStr(Common.ConvertToDbType(code), config.StringLength), isNull));
                        }
                    }
                    else
                    {
                        result.Add(string.Format("[{0}] {1} {2},", item.Name, GetDBTypeStr(Common.ConvertToDbType(code)), isNull));
                    }
                }
                else
                {
                    result.Add(string.Format("[{0}] {1} {2},", item.Name, GetDBTypeStr(Common.ConvertToDbType(code)), isNull));
                }
            }
            return result;
        }

        public List<PrimaryKey> GetPrimaryKeys(DataConfigureAttribute custAttribute)
        {
            List<PrimaryKey> primaryKey;
            if (custAttribute != null)
            {
                primaryKey = custAttribute.MetaData.Primarykey ?? new List<PrimaryKey> { new PrimaryKey { PropertyName = "ID", ColumnName = "ID" } };
            }
            else
            {
                primaryKey = new List<PrimaryKey> { new PrimaryKey { PropertyName = "ID", ColumnName = "ID" } };
            }
            return primaryKey;
        }

        protected Dictionary<string, PropertyInfo> GetProperties<T>(DataConfigureAttribute custAttribute)
        {
            var properties = new Dictionary<string, PropertyInfo>();
            if (custAttribute != null)
            {
                properties = custAttribute.MetaData.Properties;
            }
            else
            {
                typeof(T).GetProperties().Each(m => properties.Add(m.Name, m));
            }
            return properties;
        }

        #endregion
        public string ConnectionString { get; set; }
        public abstract IEnumerable<string> DataBaseTypeNames();

        protected abstract DbDataAdapter GetDbDataAdapter(DbCommand command);
        protected abstract DbConnection GetDbConnection();
        protected abstract DbCommand GetDbCommand();
        protected abstract DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter);
        protected abstract DbParameter GetDbParameter(string key, object value);

        protected virtual int ExecCommand(DbCommand command)
        {
            command.Connection = GetDbConnection();
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
                int cou = command.ExecuteNonQuery();
                return cou;
            }
            catch (Exception ex)
            {
                Logger.Info(command.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (command.Connection.State != ConnectionState.Closed)
                    command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
        }
        protected virtual void SetParameter(DbCommand comm, string key, object value)
        {
            if (key.IsNotNullAndWhiteSpace())
            {
                comm.Parameters.Add(GetDbParameter(key, value));
            }
        }

        public abstract bool IsExistTable(string tableName);
        public abstract bool IsExistColumn(string tableName, string columnName);
        public virtual int ExecuteNonQuery(string command, CommandType ct, List<KeyValuePair<string, object>> parameter)
        {
            DbCommand comm = GetDbCommand();
            comm.CommandText = command;
            comm.CommandType = ct;
            foreach (var item in parameter)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return ExecCommand(comm);
        }
        public virtual object Insert(string tableName, Dictionary<string, object> values)
        {
            if (values.Count == 0) return null;
            DbCommand command = GetDbCommand();
            command.Connection = GetDbConnection();
            StringBuilder valueBuilder = new StringBuilder();
            StringBuilder fieldBuilder = new StringBuilder();

            foreach (var item in values)
            {
                valueBuilder.AppendFormat(",{0}", item.Value == null ? "NULL" : "@" + item.Key);
                fieldBuilder.AppendFormat(",[{0}]", item.Key);
            }
            const string comInsertFormat = "INSERT INTO [{0}] ({1}) VALUES ({2})";
            command.CommandText = comInsertFormat.FormatWith(tableName, fieldBuilder.ToString().Trim(','), valueBuilder.ToString().Trim(','));
            foreach (var itemV in values)
            {
                SetParameter(command, itemV.Key, itemV.Value);
            }
            try
            {
                if (command.Connection.State != ConnectionState.Open)
                    command.Connection.Open();
                command.ExecuteNonQuery();
                command.CommandText = "SELECT @@IDENTITY";
                object cou = command.ExecuteScalar();
                return cou;
            }
            catch (Exception ex)
            {
                Logger.Info(command.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (command.Connection.State != ConnectionState.Closed)
                    command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
        }

        public virtual int Delete(string tableName, string condition, List<KeyValuePair<string, object>> keyValue, string alias = "T0")
        {
            DbCommand comm = GetDbCommand();
            comm.CommandText = !string.IsNullOrEmpty(condition) ?
                string.Format("DELETE {0} FROM [{1}] AS {0} WHERE {2}", alias, tableName, condition) :
                string.Format("DELETE {0} FROM [{1}] AS {0}", alias, tableName);

            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return ExecCommand(comm);
        }
        public virtual bool Update(string tableName, string parameterString, List<KeyValuePair<string, object>> keyValue)
        {
            DbCommand comm = GetDbCommand();
            comm.CommandText = string.Format("UPDATE [{0}] SET {1}", tableName, parameterString);
            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return ExecCommand(comm) > 0;
        }

        public virtual DataTable GetData(DbCommand command)
        {
            command.Connection = GetDbConnection();
            if (command.Connection.State != ConnectionState.Open)
                command.Connection.Open();
            DbDataAdapter adapter = GetDbDataAdapter(command);
            try
            {
                var ds = new DataSet();
                adapter.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                Logger.Info(command.CommandText);
                Logger.Error(ex);
                throw ex;
            }
            finally
            {
                if (command.Connection.State != ConnectionState.Closed)
                    command.Connection.Close();
                command.Connection.Dispose();
                command.Dispose();
            }
        }
        public virtual DataTable GetData(string selectCommand, List<KeyValuePair<string, object>> keyValue)
        {
            DbCommand comm = GetDbCommand();
            comm.CommandText = selectCommand;
            foreach (var item in keyValue)
            {
                SetParameter(comm, item.Key, item.Value);
            }
            return GetData(comm);
        }

        public virtual void AlterColumn(string tableName, string columnName, DbType columnType, int length = 255)
        {
            CustomerSql(string.Format("ALTER TABLE [{0}] ALTER COLUMN [{1}] {2}", tableName, columnName, GetDBTypeStr(columnType, length)))
                .ExecuteNonQuery();
        }
        public virtual void AddColumn(string tableName, string columnName, DbType columnType, int length = -1)
        {
            CustomerSql(string.Format("ALTER TABLE [{0}] Add COLUMN [{1}] {2}", tableName, columnName, GetDBTypeStr(columnType, length)))
                .ExecuteNonQuery();
        }
        public virtual void CreateTable<T>() where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            string tableName = GetTableName<T>(custAttribute);
            var buildColumn = new StringBuilder();
            GetCulumnSchema<T>(custAttribute).Each(m => buildColumn.Append(m));
            if (custAttribute != null && custAttribute.MetaData.Primarykey.Any())
            {
                var primaryKeyBuilder = new StringBuilder();
                custAttribute.MetaData.Primarykey.Each(m => primaryKeyBuilder.AppendFormat("[{0}],", m));
                buildColumn.Append(string.Format("PRIMARY KEY({0})", primaryKeyBuilder.ToString().Trim(',')));
            }
            CustomerSql(string.Format("Create Table [{0}] ({1}) ", tableName, buildColumn.ToString().Trim(','))).ExecuteNonQuery();
        }

        public virtual CustomerSqlHelper CustomerSql(string command)
        {
            CustomerSqlHelper customerSql = new CustomerSqlHelper(command, CommandType.Text, this);
            return customerSql;
        }
        public virtual CustomerSqlHelper StoredProcedures(string storedProcedures)
        {
            CustomerSqlHelper customerSql = new CustomerSqlHelper(storedProcedures, CommandType.StoredProcedure, this);
            return customerSql;
        }



        public virtual int Delete<T>(DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            var alias = custAttribute == null ? "T0" : custAttribute.MetaData.Alias;
            return Delete(GetTableName<T>(custAttribute), filter.ToString(), filter.GetParameterValues(), alias);
        }
        public virtual int Delete<T>(params object[] primaryKeys) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            var primaryKey = GetPrimaryKeys(custAttribute);
            if (primaryKeys.Length != primaryKey.Count)
            {
                throw new Exception("输入的参数与设置的主键个数不对应！");
            }
            var filter = new DataFilter();
            for (int i = 0; i < primaryKey.Count; i++)
            {
                filter.Where(primaryKey[i].ColumnName, OperatorType.Equal, primaryKeys[i]);
            }
            return Delete<T>(filter);
        }
        public virtual T Get<T>(params object[] primaryKeys) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            var primaryKey = GetPrimaryKeys(custAttribute);
            if (primaryKeys.Length != primaryKey.Count)
            {
                throw new Exception("输入的参数与设置的主键个数不对应！");
            }
            DataFilter filter = new DataFilter();
            for (int i = 0; i < primaryKey.Count; i++)
            {
                filter.Where(primaryKey[i].ColumnName, OperatorType.Equal, primaryKeys[i]);
            }
            var list = Get<T>(filter);
            if (list.Count() == 1)
                return list.ElementAt(0);
            return default(T);
        }
        public virtual IEnumerable<T> Get<T>(DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            if (custAttribute != null)
            {
                filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            }
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();
            string orderby = filter.GetOrderString();
            var selectCom = new StringBuilder();
            selectCom.Append("SELECT ");
            selectCom.Append(selectCol);
            selectCom.AppendFormat(" FROM [{0}] ", tableName);
            selectCom.Append(custAttribute == null ? "T0" : custAttribute.MetaData.Alias);
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    selectCom.Append(item);
                }
            }
            if (!string.IsNullOrEmpty(condition))
            {
                selectCom.AppendFormat(" WHERE {0} ", condition);
            }
            selectCom.AppendFormat(orderby);
            DataTable table = this.GetData(selectCom.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>();
            var list = new List<T>();
            Dictionary<string, PropertyInfo> properties = GetProperties<T>(custAttribute);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch, properties));
            }
            return list;

        }
        public virtual long Count<T>(DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            if (custAttribute != null)
            {
                filter = custAttribute.MetaData.DataAccess(filter);//数据权限
            }
            string tableName = GetTableName<T>(custAttribute);
            string condition = filter.ToString();
            string orderby = filter.GetOrderString();
            var selectCom = new StringBuilder();
            selectCom.Append("SELECT COUNT(1) ");
            selectCom.AppendFormat(" FROM [{0}] ", tableName);
            selectCom.Append(custAttribute == null ? "T0" : custAttribute.MetaData.Alias);
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    selectCom.Append(item);
                }
            }
            if (!string.IsNullOrEmpty(condition))
            {
                selectCom.AppendFormat(" WHERE {0} ", condition);
            }
            selectCom.AppendFormat(orderby);
            DataTable table = this.GetData(selectCom.ToString(), filter.GetParameterValues());
            if (table == null) return -1;
            return Convert.ToInt64(table.Rows[0][0]);
        }
        public virtual IEnumerable<T> Get<T>(DataFilter filter, Pagination pagin) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            string alias = "T0";
            if (custAttribute != null)
            {
                filter = custAttribute.MetaData.DataAccess(filter);//数据权限    
                alias = custAttribute.MetaData.Alias;
            }
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();

            var primaryKey = GetPrimaryKeys(custAttribute);
            foreach (var item in primaryKey)
            {
                filter.OrderBy(string.Format("[{0}].[{1}]", alias, item.ColumnName), OrderType.Ascending);
            }
            string orderby = filter.GetOrderString();
            string orderByContrary = filter.GetContraryOrderString();

            var builderRela = new StringBuilder();
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    builderRela.Append(item);
                }
            }
            const string formatCount = "SELECT COUNT(*) FROM [{0}] {3} {2} {1}";
            DataTable recordCound = this.GetData(string.Format(formatCount,
                tableName,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela,
                alias), filter.GetParameterValues());
            pagin.RecordCount = Convert.ToInt64(recordCound.Rows[0][0]);
            if (pagin.AllPage == pagin.PageIndex && pagin.RecordCount > 0)
            {
                pagin.PageIndex--;
            }
            int pageSize = pagin.PageSize;
            if ((pagin.PageIndex + 1) * pagin.PageSize > pagin.RecordCount && pagin.RecordCount != 0)
            {
                pageSize = (int)(pagin.RecordCount - pagin.PageIndex * pagin.PageSize);
                if (pageSize < 0)
                {
                    pageSize = pagin.PageSize;
                }
            }
            var builder = new StringBuilder();
            const string formatTable = "SELECT * FROM (SELECT TOP {1} * FROM (SELECT TOP {2} {0} FROM {3} {6} {5} {4}) TEMP0 {7}) TEMP1 {8}";
            builder.AppendFormat(formatTable,
                selectCol,
                pageSize,
                pagin.PageSize * (pagin.PageIndex + 1),
                string.Format("[{0}] {1}", tableName, custAttribute == null ? "T0" : custAttribute.MetaData.Alias),
                orderby,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela,
                orderByContrary.Replace("[{0}].".FormatWith(alias), ""),
                orderby.Replace("[{0}].".FormatWith(alias), ""));


            DataTable table = this.GetData(builder.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>(); ;
            var list = new List<T>();
            Dictionary<string, PropertyInfo> properties = GetProperties<T>(custAttribute);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch, properties));
            }
            return list;
        }
        public virtual void Add<T>(T item) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            if (custAttribute != null)
            {
                custAttribute.MetaData.PropertyDataConfig.Each(dic =>
                {
                    if (dic.Value.CanInsert && dic.Value.ValueProvider != null)
                    {
                        Reflection.ClassAction.SetPropertyValue(item, dic.Value.PropertyName, dic.Value.ValueProvider.GenerateValue());
                    }
                });
            }
            string tableName = GetTableName<T>(custAttribute);
            Dictionary<string, object> allValues = Reflection.ClassAction.GetPropertieValues<T>(item);
            Dictionary<string, object> insertValues = new Dictionary<string, object>();
            if (custAttribute != null)
            {
                foreach (var valueitem in allValues)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(valueitem.Key))
                    {
                        PropertyDataInfo config = custAttribute.MetaData.PropertyDataConfig[valueitem.Key];

                        if (!config.Ignore && config.CanInsert)
                        {
                            insertValues.Add(config.ColumnName, valueitem.Value);
                        }
                    }
                    else
                    {
                        insertValues.Add(valueitem.Key, valueitem.Value);
                    }
                }
            }
            else
            {
                insertValues = allValues;
            }
            object resu = this.Insert(tableName, insertValues);
            if (custAttribute != null && resu != null && !resu.Equals(0))
            {
                if (custAttribute.MetaData.Primarykey != null)
                {
                    custAttribute.MetaData.Primarykey.Each(key =>
                    {
                        if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(key.PropertyName))
                        {
                            var config = custAttribute.MetaData.PropertyDataConfig[key.PropertyName];
                            if (config.IsIncreasePrimaryKey && config.ValueProvider == null)
                            {
                                Reflection.ClassAction.SetPropertyValue(item, key.PropertyName, resu);
                            }
                        }
                    });
                }
            }
        }
        public virtual bool Update<T>(T item, DataFilter filter) where T : class
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();

            if (custAttribute != null)
            {
                custAttribute.MetaData.PropertyDataConfig.Each(dic =>
                {
                    if (dic.Value.CanUpdate && dic.Value.ValueProvider != null)
                    {
                        Reflection.ClassAction.SetPropertyValue(item, dic.Value.PropertyName, dic.Value.ValueProvider.GenerateValue());
                    }
                });
            }

            string tableName = GetTableName<T>(custAttribute);
            PropertyInfo[] propertys = typeof(T).GetProperties();
            StringBuilder builder = new StringBuilder();
            List<KeyValuePair<string, object>> keyValue = new List<KeyValuePair<string, object>>();
            foreach (var property in propertys)
            {
                if (filter.UpdateProperties != null && !filter.UpdateProperties.Contains(property.Name))
                {
                    continue;
                }
                string name = property.Name;
                object value = property.GetValue(item, null);

                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(name))
                    {
                        PropertyDataInfo data = custAttribute.MetaData.PropertyDataConfig[name];
                        if (data.IsRelation || data.Ignore || !data.CanUpdate)
                            continue;
                        if (!string.IsNullOrEmpty(data.ColumnName))
                        {
                            name = data.ColumnName;
                        }
                    }
                }
                if (value != null)
                {
                    builder.AppendFormat(builder.Length == 0 ? "[{0}]=@{0}" : ",[{0}]=@{0}", name);
                    KeyValuePair<string, object> kv = new KeyValuePair<string, object>(name, value);
                    keyValue.Add(kv);
                }
                else
                {
                    builder.AppendFormat(builder.Length == 0 ? "[{0}]=NULL" : ",[{0}]=NULL", name);
                }

            }
            if (builder.Length == 0) return false;
            string condition = filter.ToString();
            if (!string.IsNullOrEmpty(condition))
            {
                builder.Append(" WHERE ");
                builder.Append(condition);
            }
            keyValue.AddRange(filter.GetParameterValues());
            return Update(tableName, builder.ToString(), keyValue);

        }
        public virtual bool Update<T>(T item, params object[] primaryKeys) where T : class
        {
            DataFilter filter = new DataFilter();
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            List<PrimaryKey> primaryKey = GetPrimaryKeys(custAttribute);

            if (primaryKeys.Length != primaryKey.Count && primaryKeys.Length > 0)
            {
                throw new Exception("输入的参数与设置的主键个数不对应！");
            }
            if (primaryKeys.Length > 0)
            {
                for (int i = 0; i < primaryKey.Count; i++)
                {
                    filter.Where(primaryKey[i].ColumnName, OperatorType.Equal, primaryKeys[i]);
                }
            }
            else
            {
                Type entityType = typeof(T);

                foreach (var primary in primaryKey)
                {
                    PropertyInfo proper = entityType.GetProperty(primary.PropertyName);
                    if (proper != null && proper.CanRead)
                    {
                        filter.Where(primary.ColumnName, OperatorType.Equal, proper.GetValue(item, null));
                    }
                }
            }
            return Update(item, filter);

        }


    }
}
