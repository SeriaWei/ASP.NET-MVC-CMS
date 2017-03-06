/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Extend
{
    public static class ExtTable
    {
        /// <summary>
        /// 获取表里某页的数据
        /// </summary>
        /// <param name="data">表数据</param>
        /// <param name="PageIndex">当前页</param>
        /// <param name="PageSize">分页大小</param>
        /// <param name="AllPage">返回总页数</param>
        /// <returns>返回当页表数据</returns>
        public static System.Data.DataTable GetPage(this System.Data.DataTable data, int PageIndex, int PageSize, out int AllPage)
        {
            AllPage = data.Rows.Count / PageSize;
            AllPage += data.Rows.Count % PageSize == 0 ? 0 : 1;
            System.Data.DataTable Ntable = data.Clone();
            int startIndex = PageIndex * PageSize;
            int endIndex = startIndex + PageSize > data.Rows.Count ? data.Rows.Count : startIndex + PageSize;
            if (startIndex < endIndex)
                for (int i = startIndex; i < endIndex; i++)
                {
                    Ntable.ImportRow(data.Rows[i]);
                }
            return Ntable;
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回字符串类型</returns>
        public static string GetValue_String(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToString(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回字符串类型</returns>
        public static string GetValue_String(this System.Data.DataTable table, int Row, int Collum)
        {
            return Convert.ToString(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回长整形</returns>
        public static long GetValue_Long(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToInt64(table.Rows[Row][Collum]);
        }
        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Coll">列索引</param>
        /// <returns>返回长整形</returns>
        public static long GetValue_Long(this System.Data.DataTable table, int Row, int Coll)
        {
            return Convert.ToInt64(table.Rows[Row][Coll]);
        }
        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回日期</returns>
        public static DateTime GetValue_DateTime(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToDateTime(table.Rows[Row][Collum]);
        }
        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回日期</returns>
        public static DateTime GetValue_DateTime(this System.Data.DataTable table, int Row, int Collum)
        {
            return Convert.ToDateTime(table.Rows[Row][Collum]);
        }
        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回整形</returns>
        public static int GetValue_Int(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToInt32(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回整形</returns>
        public static int GetValue_Int(this System.Data.DataTable table, int Row, int Collum)
        {
            return Convert.ToInt32(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回双精度型</returns>
        public static double GetValue_Double(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToDouble(table.Rows[Row][Collum]);
        }
        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回双精度型</returns>
        public static double GetValue_Double(this System.Data.DataTable table, int Row, int Collum)
        {
            return Convert.ToDouble(table.Rows[Row][Collum]);
        }
        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static decimal GetValue_Decimal(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToDecimal(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static decimal GetValue_Decimal(this System.Data.DataTable table, int Row, int Collum)
        {
            return Convert.ToDecimal(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static bool GetValue_Boolean(this System.Data.DataTable table, int Row, string Collum)
        {
            return Convert.ToBoolean(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 返回表中数据
        /// </summary>
        /// <param name="table">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static bool GetValue_Boolean(this System.Data.DataTable table, int Row, int Collum)
        {
            return Convert.ToBoolean(table.Rows[Row][Collum]);
        }

        /// <summary>
        /// 获取叶结点
        /// </summary>
        /// <param name="table">数据源</param>
        /// <param name="TopID">顶级ID值</param>
        /// <param name="IDColl">ID列名称</param>
        /// <param name="PIDColl">父级ID列名称</param>
        /// <returns>反回列结点值</returns>
        public static System.Data.DataTable GetLastNode(this System.Data.DataTable table, int TopID, string IDColl, string PIDColl)
        {
            System.Data.DataTable reTable = table.Clone();
            FindLastNode(reTable, table, TopID, IDColl, PIDColl);
            return reTable;
        }

        private static void FindLastNode(System.Data.DataTable tableRe, System.Data.DataTable tableSource, int PID, string IDColl, string PIDColl)
        {
            System.Data.DataRow[] rows = tableSource.Select(PIDColl + "=" + PID);
            if (rows.Length == 0)
            {
                System.Data.DataRow[] LNode = tableSource.Select(IDColl + "=" + PID);
                foreach (System.Data.DataRow item in LNode)
                {
                    tableRe.ImportRow(item);
                }
            }
            else
            {
                foreach (System.Data.DataRow item in rows)
                {
                    FindLastNode(tableRe, tableSource, Convert.ToInt32(item[IDColl]), IDColl, PIDColl);
                }
            }
        }

    }
}
