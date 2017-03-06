/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Models;

namespace Easy.Extend
{
    public static class ExtList
    {
        /// <summary>
        /// 转成用于自动完成的实例的List
        /// </summary>
        /// <typeparam name="T">原实例类型</typeparam>
        /// <param name="list">原数据</param>
        /// <param name="value">值</param>
        /// <param name="text">文本</param>
        /// <returns></returns>
        public static List<AutoComplete> ToAutoComplete<T>(this IEnumerable<T> list, Func<T, string> value, Func<T, string> text)
        {
            List<AutoComplete> completes = new List<AutoComplete>();
            foreach (var item in list)
            {
                completes.Add(new AutoComplete() { Text = text(item), Value = value(item) });
            }
            return completes;
        }
    }
}
