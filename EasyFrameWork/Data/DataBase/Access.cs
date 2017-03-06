/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;
using Easy.Extend;

namespace Easy.Data.DataBase
{
    public abstract class Access : DataBasic
    {

        protected override DbDataAdapter GetDbDataAdapter(DbCommand command)
        {
            return new OleDbDataAdapter(command as OleDbCommand);
        }



        protected override DbCommand GetDbCommand()
        {
            return new OleDbCommand();
        }

        protected override DbCommandBuilder GetDbCommandBuilder(DbDataAdapter adapter)
        {
            return new OleDbCommandBuilder(adapter as OleDbDataAdapter);
        }

        protected override DbParameter GetDbParameter(string key, object value)
        {
            return new OleDbParameter(key, value);
        }

        protected override void SetParameter(DbCommand comm, string key, object value)
        {
            if (key.IsNotNullAndWhiteSpace())
            {
                if (value is DateTime)
                {
                    comm.Parameters.Add(new OleDbParameter
                    {
                        OleDbType = OleDbType.DBTimeStamp,
                        Value = Convert.ToDateTime(value).ToString("yyyy-MM-dd HH:mm:ss")
                    });
                }
                else
                {
                    base.SetParameter(comm, key, value);
                }
            }
        }

        public override bool IsExistTable(string tableName)
        {
            var conn = GetDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            var table = (conn as OleDbConnection).GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, tableName });
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            return table.Rows.Count != 0;
        }

        public override bool IsExistColumn(string tableName, string columnName)
        {
            var conn = GetDbConnection();
            if (conn.State != ConnectionState.Open)
                conn.Open();
            var table = (conn as OleDbConnection).GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, tableName, columnName });
            if (conn.State != ConnectionState.Closed)
                conn.Close();
            return table.Rows.Count != 0;
        }

    }
}
