/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Extend
{
    public static class ExtRow
    {
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回字符串类型</returns>
        public static string GetValue_String(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToString(row[Collum]);
        }

        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回字符串类型</returns>
        public static string GetValue_String(this System.Data.DataRow row, int Collum)
        {
            return Convert.ToString(row[Collum]);
        }

        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回长整形</returns>
        public static long GetValue_Long(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToInt64(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Coll">列索引</param>
        /// <returns>返回长整形</returns>
        public static long GetValue_Long(this System.Data.DataRow row, int Coll)
        {
            return Convert.ToInt64(row[Coll]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回日期</returns>
        public static DateTime GetValue_DateTime(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToDateTime(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回日期</returns>
        public static DateTime GetValue_DateTime(this System.Data.DataRow row, int Row, int Collum)
        {
            return Convert.ToDateTime(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回整形</returns>
        public static int GetValue_Int(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToInt32(row[Collum]);
        }

        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回整形</returns>
        public static int GetValue_Int(this System.Data.DataRow row, int Collum)
        {
            return Convert.ToInt32(row[Collum]);
        }

        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回双精度型</returns>
        public static double GetValue_Double(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToDouble(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回双精度型</returns>
        public static double GetValue_Double(this System.Data.DataRow row, int Collum)
        {
            return Convert.ToDouble(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static decimal GetValue_Decimal(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToDecimal(row[Collum]);
        }

        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static decimal GetValue_Decimal(this System.Data.DataRow row, int Collum)
        {
            return Convert.ToDecimal(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static bool GetValue_Boolean(this System.Data.DataRow row, string Collum)
        {
            return Convert.ToBoolean(row[Collum]);
        }
        /// <summary>
        /// 返回行中数据
        /// </summary>
        /// <param name="row">表数据</param>
        /// <param name="Row">行索引</param>
        /// <param name="Collum">列名称</param>
        /// <returns>返回货币类型</returns>
        public static bool GetValue_Boolean(this System.Data.DataRow row, int Collum)
        {
            return Convert.ToBoolean(row[Collum]);
        }
    }
}
