using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;

namespace Easy.Data
{
    public class ExcelReader
    {
        OleDbConnection objConn;
        public ExcelReader(string file)
        {
            objConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + file + ";" + "Extended Properties=Excel 8.0;");
        }
        /// <summary>
        /// 获取第一个sheet数据
        /// </summary>
        /// <returns></returns>
        public DataTable GetData()
        {
            List<string> tables = GetTables();
            return GetData(tables[0]);
        }
        /// <summary>
        /// 获取对应sheet数据
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataTable GetData(string tableName)
        {
            return ExcTable("select * from [" + tableName + "]");
        }

        /// <summary>
        /// 获取所有sheet
        /// </summary>
        /// <returns></returns>
        public List<string> GetTables()
        {
            List<string> tables = new List<string>();
            objConn.Open();
            DataTable schemaTable = objConn.GetOleDbSchemaTable(System.Data.OleDb.OleDbSchemaGuid.Tables, null);
            objConn.Close();
            foreach (System.Data.DataRow item in schemaTable.Rows)
            {
                string tableName = item[2].ToString();
                if (tableName.EndsWith("$"))
                {
                    tables.Add(item[2].ToString());
                }
            }
            return tables;
        }
        /// <summary>
        /// 获取第一个sheet的列信息
        /// </summary>
        /// <returns></returns>
        public DataColumnCollection GetColumns()
        {
            List<string> tables = GetTables();
            return GetColumns(tables[0]);
        }
        /// <summary>
        /// 获取对应sheet的列信息
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public DataColumnCollection GetColumns(string tableName)
        {
            DataTable table = ExcTable("select top 1 * from [" + tableName + "]");
            return table.Columns;
        }

        private DataTable ExcTable(string comm)
        {
            DataSet ds = new DataSet();
            objConn.Open();
            string strSql = comm;
            OleDbCommand objCmd = new OleDbCommand(strSql, objConn);
            OleDbDataAdapter myData = new OleDbDataAdapter(strSql, objConn);
            myData.Fill(ds);
            objConn.Close();
            return ds.Tables[0];
        }
    }
}
