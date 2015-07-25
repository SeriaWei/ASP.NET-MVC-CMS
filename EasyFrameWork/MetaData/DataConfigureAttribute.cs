using Easy.Cache;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.MetaData
{
    /// <summary>
    /// 数据和视图的配置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class DataConfigureAttribute : System.Attribute
    {
        public IDataViewMetaData MetaData
        {
            get;
            private set;
        }
        public DataConfigureAttribute(Type MetaDataType)
        {
            IDataViewMetaData metaData = Activator.CreateInstance(MetaDataType) as IDataViewMetaData;
            //HTML标签的多语言
            Dictionary<string, string> lan = new Dictionary<string, string>();
            foreach (var item in metaData.HtmlTags)
            {
                if (string.IsNullOrEmpty(item.Value.DisplayName))
                    lan.Add(item.Key, item.Value.ModelType.Name + "@" + item.Key);
            }
            lan = Localization.InitLan(lan);
            foreach (var item in lan)
            {
                metaData.HtmlTags[item.Key].DisplayName = item.Value;
            }
            this.MetaData = metaData;
        }
        /// <summary>
        /// 获取属性对表的映射
        /// </summary>
        /// <param name="Property"></param>
        /// <returns></returns>
        public string GetPropertyMapper(string Property)
        {
            if (this.MetaData.PropertyDataConfig.ContainsKey(Property))
            {
                PropertyDataInfo config = this.MetaData.PropertyDataConfig[Property];
                if (!string.IsNullOrEmpty(config.ColumnName))
                {
                    string alias = config.IsRelation ? config.TableAlias : this.MetaData.Alias;
                    return string.Format("[{0}].[{1}]", alias, config.ColumnName);
                }
                else
                {
                    string alias = config.IsRelation ? config.TableAlias : this.MetaData.Alias;
                    return string.Format("[{0}].[{1}]", alias, Property);
                }
            }
            else return Property;
        }

        /// <summary>
        /// 获取所有HTML标签
        /// </summary>
        /// <returns></returns>
        public List<HTML.Tags.HtmlTagBase> GetHtmlTags(bool widthHidden)
        {
            List<HTML.Tags.HtmlTagBase> returnValue = new List<HTML.Tags.HtmlTagBase>();
            foreach (var item in MetaData.HtmlTags)
            {
                if (item.Value.IsIgnore)
                    continue;
                if ((item.Value.TagType == HTML.HTMLEnumerate.HTMLTagTypes.Hidden || item.Value.IsHidden) && !widthHidden)
                    continue;
                returnValue.Add(item.Value);
            }
            return returnValue.OrderBy(q => q.OrderIndex).ToList();
        }
        /// <summary>
        /// 获取HTML标签
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public HTML.Tags.HtmlTagBase GetHtmlTag(string property)
        {
            if (MetaData.HtmlTags.ContainsKey(property))
            {
                return MetaData.HtmlTags[property];
            }
            else return new HTML.Tags.TextBoxHtmlTag(MetaData.TargetType, property);
        }
        public HTML.Tags.HtmlTagBase GetHtmlTag<T>(System.Linq.Expressions.Expression<Func<T, object>> expression)
        {
            string property = Easy.Common.GetLinqExpressionText(expression);
            return this.GetHtmlTag(property);
        }
        /// <summary>
        /// 获取显示名称
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        public string GetDisplayName(string property)
        {
            if (MetaData.HtmlTags.ContainsKey(property))
            {
                return MetaData.HtmlTags[property].DisplayName;
            }
            else return property;
        }
        /// <summary>
        /// 获取隐藏域标签
        /// </summary>
        /// <returns></returns>
        public List<HTML.Tags.HtmlTagBase> GetHtmlHiddenTags()
        {
            List<HTML.Tags.HtmlTagBase> returnValue = new List<HTML.Tags.HtmlTagBase>();
            foreach (var item in MetaData.HtmlTags)
            {
                if (item.Value.IsIgnore) continue;
                if (item.Value.TagType == HTML.HTMLEnumerate.HTMLTagTypes.Hidden || item.Value.IsHidden)
                    returnValue.Add(item.Value);
            }
            return returnValue.OrderBy(q => q.OrderIndex).ToList();
        }
        /// <summary>
        /// 获取数据配置的特性
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static DataConfigureAttribute GetAttribute<T>()
        {
            Type targetType = typeof(T);
            StaticCache cache = new StaticCache();
            string typeName = targetType.FullName;
            var attribute = cache.Get("DataConfigureAttribute_" + typeName, m =>
                {
                    return
                        System.Attribute.GetCustomAttribute(targetType, typeof(DataConfigureAttribute)) as
                            DataConfigureAttribute;
                });
            if (attribute != null)
            {
                attribute.GetHtmlTags(true).ForEach(m => m.ResetValue());
            }
            return attribute;
        }
        public static DataConfigureAttribute GetAttribute(Type type)
        {
            StaticCache cache = new StaticCache();
            string typeName = type.FullName;
            var attribute = cache.Get("DataConfigureAttribute_" + typeName, m =>
            {
                return
                    System.Attribute.GetCustomAttribute(type, typeof(DataConfigureAttribute)) as
                        DataConfigureAttribute;
            });
            if (attribute != null)
            {
                attribute.GetHtmlTags(true).ForEach(m => m.ResetValue());
            }
            return attribute;
        }
    }
}
