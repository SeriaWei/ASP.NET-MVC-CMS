/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Easy.Data.DataBase
{
    public class SqlEarly : DataBasic
    {
        public SqlEarly()
        {
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings[ConnectionKey].ConnectionString;
        }
        public override IEnumerable<string> DataBaseTypeNames()
        {
            yield return "SQL-Early";
        }

        protected override DbDataAdapter GetDbDataAdapter(DbCommand command)
        {
            return new SqlDataAdapter(command as SqlCommand);
        }

        protected override DbConnection GetDbConnection()
        {
            return new SqlConnection(ConnectionString);
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

        public override bool IsExistTable(string tableName)
        {
            return CustomerSql("SELECT COUNT(1) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME=@tableName")
                 .AddParameter("@tableName", tableName)
                 .To<int>() != 0;
        }

        public override bool IsExistColumn(string tableName, string columnName)
        {
            return CustomerSql("SELECT COUNT(*) FROM INFORMATION_SCHEMA.[COLUMNS]  WHERE TABLE_NAME=@tableName AND COLUMN_NAME=@columnName")
                .AddParameter("@tableName", tableName)
                .AddParameter("@columnName", columnName)
                .To<int>() != 0;
        }

    }
}
