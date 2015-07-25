using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Data.Common;

namespace Easy.Data.DataBase
{
    public class Access : DataBasic
    {
        const string AceOleDb = "Provider=Microsoft.ACE.OLEDB.12.0;";
        const string JetOleDb = "Provider=Microsoft.Jet.OLEDB.4.0;";
        public enum DdTypes
        {
            /// <summary>
            /// 2007以前版本
            /// </summary>
            JET = 1,
            /// <summary>
            /// 2007以后版本
            /// </summary>
            Ace = 2
        }
        public Access()
        {
            DataPath = AppDomain.CurrentDomain.BaseDirectory + @"App_Data\DataBase.accdb";
            this.DbType = DdTypes.JET;
        }

        public Access(string filePath)
        {
            if (filePath.Contains(":"))
            {
                DataPath = filePath;
            }
            else
            {
                DataPath = AppDomain.CurrentDomain.BaseDirectory + filePath;
            }
            this.DbType = DdTypes.JET;
        }
        public DdTypes DbType { get; set; }
        private string DataPath { get; set; }

        protected override DbDataAdapter GetDbDataAdapter(DbCommand command)
        {
            return new OleDbDataAdapter(command as OleDbCommand);
        }

        protected override DbConnection GetDbConnection()
        {
            if (this.DbType == DdTypes.JET)
            {
                var conn = new OleDbConnection(string.Format("{1};Data Source={0};persist security info=false;Jet OLEDB:Database Password=waynewei123", DataPath, JetOleDb));
                return conn;
            }
            else
            {
                var conn = new OleDbConnection(string.Format("{1};Data Source={0};persist security info=false;Jet OLEDB:Database Password=waynewei123", DataPath, AceOleDb));
                return conn;
            }
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
