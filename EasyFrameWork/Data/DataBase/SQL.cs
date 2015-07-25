using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace Easy.Data.DataBase
{
    public class SQL : DataBasic
    {

        public SQL()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }

        public SQL(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; set; }


        protected override DbConnection GetDbConnection()
        {
            return new SqlConnection(ConnectionString);
        }


        protected override DbDataAdapter GetDbDataAdapter(DbCommand command)
        {
            return new SqlDataAdapter(command as SqlCommand);
        }

        protected override DbCommand GetDbCommand()
        {
            return new SqlCommand();
        }

        protected override DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter)
        {
            return new SqlCommandBuilder(adapter as SqlDataAdapter);
        }

        protected override DbParameter GetDbParameter(string key, object value)
        {
            return new SqlParameter(key, value);
        }
        public override List<T> Get<T>(DataFilter filter, Pagination pagin)
        {
            MetaData.DataConfigureAttribute custAttribute = Easy.MetaData.DataConfigureAttribute.GetAttribute<T>();
            string tableName = GetTableName<T>(custAttribute);
            List<KeyValuePair<string, string>> comMatch;
            string selectCol = GetSelectColumn<T>(custAttribute, out comMatch);
            string condition = filter.ToString();
            Dictionary<int, string> primaryKey = GetPrimaryKeys(custAttribute);
            foreach (int item in primaryKey.Keys)
            {
                filter.OrderBy(primaryKey[item], OrderType.Ascending);
            }
            string orderby = filter.GetOrderString();
            StringBuilder builder = new StringBuilder();
            builder.Append("WITH T AS( ");
            builder.AppendFormat("SELECT TOP {0} ", pagin.PageSize * (pagin.PageIndex + 1));
            builder.Append(selectCol);
            builder.AppendFormat(",ROW_NUMBER() OVER ({0}) AS RowIndex ", orderby);
            builder.AppendFormat(" FROM [{0}] ", tableName);
            builder.Append(custAttribute == null ? "T0" : custAttribute.MetaData.Alias);
            StringBuilder builderRela = new StringBuilder();
            if (custAttribute != null)
            {
                foreach (var item in custAttribute.MetaData.DataRelations)
                {
                    builder.Append(item);
                    builderRela.Append(item);
                }
            }
            builder.Append(string.IsNullOrEmpty(condition) ? "" : " WHERE " + condition);
            builder.AppendFormat(") SELECT {0} FROM T WHERE RowIndex>{1} AND RowIndex<={2}", selectCol, pagin.PageIndex * pagin.PageSize, pagin.PageSize * (pagin.PageIndex + 1));
            DataTable table = this.GetTable(builder.ToString(), filter.GetParameterValues());
            if (table == null) return new List<T>();
            List<T> list = new List<T>();
            Dictionary<string, PropertyInfo> properties = GetProperties<T>(custAttribute);
            for (int i = 0; i < table.Rows.Count; i++)
            {
                list.Add(Reflection.ClassAction.GetModel<T>(table, i, comMatch, properties));
            }
            DataTable recordCound = this.GetTable(string.Format("SELECT COUNT(1) FROM [{0}] {3} {2} {1}",
                tableName,
                string.IsNullOrEmpty(condition) ? "" : "WHERE " + condition,
                builderRela.ToString(),
                custAttribute == null ? "T0" : custAttribute.MetaData.Alias), filter.GetParameterValues());
            pagin.RecordCount = Convert.ToInt64(recordCound.Rows[0][0]);
            return list;

        }


        public override bool IsExistTable(string tableName)
        {
            return
                CustomerSql("SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='" + tableName + "'")
                    .To<int>() != 0;
        }

        public override bool IsExistColumn(string tableName, string columnName)
        {
            return CustomerSql("SELECT COUNT(*) FROM INFORMATION_SCHEMA.[COLUMNS]  WHERE TABLE_NAME='" + tableName + "' AND COLUMN_NAME='" + columnName + "'").To<int>() != 0;
        }
    }

}
