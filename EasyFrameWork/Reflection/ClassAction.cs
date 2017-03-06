/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Reflection
{
    public class ClassAction
    {
        /// <summary>
        /// 取出类的属性值
        /// </summary>
        /// <typeparam name="T">类别</typeparam>
        /// <param name="obj">对象</param>
        /// <returns>返回字典</returns>
        public static Dictionary<string, object> GetPropertieValues<T>(T obj)
        {
            Type objType = typeof(T);
            var properties = objType.GetProperties();
            return properties.Where(item => item.GetValue(obj, null) != null).ToDictionary(item => item.Name, item => item.GetValue(obj, null));
        }
        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="item">类型实例</param>
        /// <param name="property">属性名称</param>
        /// <returns>属性值</returns>
        public static object GetPropertyValue<T>(T item, string property)
        {
            Type entityType = typeof(T);
            PropertyInfo proper = entityType.GetProperty(property);
            if (proper != null && proper.CanRead)
            {
                return proper.GetValue(item, null);
            }
            else return null;
        }

        public static object GetObjPropertyValue(object item, string property)
        {
            Type entityType = item.GetType();
            PropertyInfo proper = entityType.GetProperty(property);
            if (proper != null && proper.CanRead)
            {
                return proper.GetValue(item, null);
            }
            else return null;
        }

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item"></param>
        /// <param name="property"></param>
        /// <param name="value"></param>
        public static void SetPropertyValue<T>(T item, string property, object value)
        {
            Type entityType = typeof(T);
            PropertyInfo proper = entityType.GetProperty(property);
            if (proper != null && proper.CanWrite)
            {
                proper.SetValue(item, ValueConvert(proper, value), null);
            }
        }

        public static void SetObjPropertyValue(object obj, string property, object value)
        {
            Type entityType = obj.GetType();
            PropertyInfo proper = entityType.GetProperty(property);
            if (proper != null && proper.CanWrite)
            {
                proper.SetValue(obj, ValueConvert(proper, value), null);
            }
        }

        /// <summary>
        /// 初始化Model,自动对对应属性赋值
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <param name="data">数据</param>
        /// <returns>返回Model对象</returns>
        public static T GetModel<T>(System.Data.DataTable data, int RowIndex)
        {
            if (data == null || data.Rows.Count == 0)
                return default(T);
            Type ty = typeof(T);
            T obj;
            if (ty.IsClass && ty.Name != "String")
            {
                obj = ServiceLocator.Current.GetInstance<T>();
                var properties = ty.GetProperties();
                if (properties.Any())
                {
                    foreach (var item in properties)
                    {
                        if (data.Columns.Contains(item.Name) && data.Rows[RowIndex][item.Name].GetType() != typeof(DBNull) && item.CanWrite)
                            item.SetValue(obj, data.Rows[RowIndex][item.Name], null);
                    }
                }
                else
                {
                    obj = (T)ValueConvert(ty, data.Rows[RowIndex][0]);
                }
            }
            else
            {
                obj = (T)ValueConvert(ty, data.Rows[RowIndex][0]);
            }
            return obj;

        }
        /// <summary>
        /// 获取实例对象
        /// </summary>
        /// <typeparam name="T">实例类型</typeparam>
        /// <param name="data">数据</param>
        /// <param name="RowIndex">行</param>
        /// <param name="columns">数据列与实例属性的匹配关系,Key为表列名,Value为属性名</param>
        /// <returns>实例对象</returns>
        public static T GetModel<T>(System.Data.DataTable data, int RowIndex, List<KeyValuePair<string, string>> columns)
        {
            if (data == null || data.Rows.Count == 0)
                return default(T);
            Type ty = typeof(T);
            var obj = ServiceLocator.Current.GetInstance<T>();
            foreach (var item in columns)
            {
                if (!data.Columns.Contains(item.Key))
                    continue;
                if (!(data.Rows[RowIndex][item.Key] is DBNull))
                {
                    PropertyInfo property = ty.GetProperty(item.Value);
                    if (property.CanWrite)
                    {
                        property.SetValue(obj, ValueConvert(property, data.Rows[RowIndex][item.Key]), null);
                    }
                }
            }
            return obj;
        }
        public static T GetModel<T>(System.Data.DataTable data, int RowIndex, List<KeyValuePair<string, string>> columns, Dictionary<string, PropertyInfo> properties)
        {
            if (data == null || data.Rows.Count == 0)
                return default(T);
            var obj = ServiceLocator.Current.GetInstance<T>();
            foreach (var item in columns)
            {
                if (!data.Columns.Contains(item.Key))
                    continue;
                if (!(data.Rows[RowIndex][item.Key] is DBNull) && properties.ContainsKey(item.Value))
                {
                    PropertyInfo property = properties[item.Value];
                    if (property.CanWrite)
                    {
                        property.SetValue(obj, ValueConvert(property, data.Rows[RowIndex][item.Key]), null);
                    }
                }
            }
            return obj;
        }
        /// <summary>
        ///  初始化Model,自动对对应属性赋值
        /// </summary>
        /// <typeparam name="T">Model的类型</typeparam>
        /// <param name="collection">数据集合</param>
        /// <returns>返回Model对象</returns>
        public static T GetModel<T>(NameValueCollection collection) where T : class
        {
            Type objType = typeof(T);
            var obj = ServiceLocator.Current.GetInstance<T>();
            var properties = objType.GetProperties();
            foreach (string key in collection.AllKeys)
            {
                foreach (var property in properties)
                {
                    if (property.Name.Equals(key))
                    {
                        string value = collection[key];
                        if (!string.IsNullOrEmpty(value) && property.CanWrite)
                            property.SetValue(obj, ValueConvert(property, value), null);
                    }
                }
            }
            return obj;
        }
        public static object GetModel(Type target, NameValueCollection collection)
        {
            object obj = Activator.CreateInstance(target);
            var properties = target.GetProperties();
            foreach (string key in collection.AllKeys)
            {
                foreach (var property in properties)
                {
                    if (property.Name.Equals(key))
                    {
                        string value = collection[key];
                        if (!string.IsNullOrEmpty(value) && property.CanWrite)
                            property.SetValue(obj, ValueConvert(property, value), null);
                    }
                }
            }
            return obj;
        }
        /// <summary>
        /// 初始化Model,自动对对应属性赋值
        /// </summary>
        /// <typeparam name="T">Model类型</typeparam>
        /// <param name="collection">数据集合</param>
        /// <param name="replaceKey">要替换掉KEY的部分</param>
        /// <returns>Model对象</returns>
        public static T GetModel<T>(NameValueCollection collection, string replaceKey) where T : class
        {
            Type objType = typeof(T);
            var obj = ServiceLocator.Current.GetInstance<T>();
            var properties = objType.GetProperties();
            foreach (string key in collection.AllKeys)
            {
                foreach (var property in properties)
                {
                    if (property.Name.Equals(key.Replace(replaceKey, "")))
                    {
                        string value = collection[key];
                        if (!string.IsNullOrEmpty(value) && property.CanWrite)
                            property.SetValue(obj, ValueConvert(property, value), null);
                    }
                }
            }
            return obj;
        }

        public static object GetModel(Type target, NameValueCollection collection, string replaceKey)
        {
            object obj = ServiceLocator.Current.GetInstance(target);
            var properties = target.GetProperties();
            foreach (string key in collection.AllKeys)
            {
                foreach (var property in properties)
                {
                    if (property.Name.Equals(key.Replace(replaceKey, "")))
                    {
                        string value = collection[key];
                        if (!string.IsNullOrEmpty(value) && property.CanWrite)
                            property.SetValue(obj, ValueConvert(property, value), null);
                    }
                }
            }
            return obj;
        }
        public static object ValueConvert(PropertyInfo property, object obj)
        {
            return ValueConvert(property.PropertyType, obj);
        }
        public static object ValueConvert(Type type, object obj)
        {
            if (obj == null) return null;
            TypeCode code = type.IsGenericType ? Type.GetTypeCode(type.GetGenericArguments()[0]) : Type.GetTypeCode(type);
            switch (code)
            {
                case TypeCode.Boolean:
                    {
                        if (obj != null)
                        {
                            string result = obj.ToString().ToLower();
                            if (result == "true" || result == "1")
                                return true;
                            else return false;
                        }
                        else return false;
                    }
                case TypeCode.Byte: return Convert.ToByte(obj);
                case TypeCode.Char: return Convert.ToChar(obj);
                case TypeCode.DBNull: return null;
                case TypeCode.DateTime: return Convert.ToDateTime(obj);
                case TypeCode.Decimal: return Convert.ToDecimal(obj);
                case TypeCode.Double: return Convert.ToDouble(obj);
                case TypeCode.Empty: return null;
                case TypeCode.Int16: return Convert.ToInt16(obj);
                case TypeCode.Int32: return Convert.ToInt32(obj);
                case TypeCode.Int64: return Convert.ToInt64(obj);
                case TypeCode.Object: return obj;
                case TypeCode.SByte: return Convert.ToSByte(obj);
                case TypeCode.Single: return Convert.ToSingle(obj);
                case TypeCode.String: return Convert.ToString(obj);
                case TypeCode.UInt16: return Convert.ToUInt16(obj);
                case TypeCode.UInt32: return Convert.ToUInt32(obj);
                case TypeCode.UInt64: return Convert.ToUInt64(obj);
                default: return obj;
            }
        }
        public static void CopyProperty(object obj, object toObj)
        {
            PropertyDescriptorCollection objPropertites = TypeDescriptor.GetProperties(obj);
            PropertyDescriptorCollection toObjPropertites = TypeDescriptor.GetProperties(toObj);

            foreach (PropertyDescriptor item in objPropertites)
            {
                if (toObjPropertites.Contains(item))
                {
                    toObjPropertites[item.Name].SetValue(toObj, item.GetValue(obj));
                }
            }
        }
    }
}
