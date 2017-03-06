/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Data;
using Easy.ViewPort.Descriptor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using Easy.MetaData;
using System.Web;
using Easy.Extend;
using Easy.Constant;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Easy.ViewPort.Grid
{
    public class GridData
    {
        private readonly NameValueCollection _form;
        private readonly Func<BaseDescriptor, Dictionary<string, string>> _dropDownDataFunc;

        private static readonly Regex ConditionIndexRegex = new Regex(@"Conditions\[(\d+)\]", RegexOptions.Compiled);
        private static readonly Regex GroupIndexRegex = new Regex(@"ConditionGroups\[(\d+)\]", RegexOptions.Compiled);
        private static readonly Regex GroupConditoinIndexRegex = new Regex(@"\[Conditions\]\[(\d+)\]", RegexOptions.Compiled);
        private static readonly Regex PropertyRegex = new Regex(@"\[([a-z|A-Z]*)\]$", RegexOptions.Compiled);
        private PropertyInfo[] _propertyInfos;

        Dictionary<string, Dictionary<string, string>> _options;
        public Dictionary<string, Dictionary<string, string>> DropDownOptions
        {
            get { return _options ?? (_options = new Dictionary<string, Dictionary<string, string>>()); }
            set { _options = value; }
        }

        public GridData(NameValueCollection form)
        {
            this._form = form;
        }
        public GridData(NameValueCollection form, Func<BaseDescriptor, Dictionary<string, string>> dropDownListData)
        {
            this._form = form;
            _dropDownDataFunc = dropDownListData;
        }
        #region 私有方法
        private string GetModelString(object source, DataConfigureAttribute attribute)
        {
            Type tType = source.GetType();
            System.Reflection.PropertyInfo[] propertys = tType.GetProperties();
            StringBuilder columnBuilder = new StringBuilder();
            columnBuilder.Append("{");
            foreach (var item in propertys)
            {
                object value = item.GetValue(source, null);
                if ((attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.DropDownList ||
                    attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.MutiSelect)
                    && value != null)
                {
                    var tag = (DropDownListDescriptor)attribute.MetaData.ViewPortDescriptors[item.Name];
                    IDictionary<string, string> options = tag.OptionItems;
                    if (tag.SourceType == SourceType.ViewData)
                    {
                        if (_dropDownDataFunc != null)
                        {
                            options = _dropDownDataFunc(tag);
                        }
                    }
                    if (options != null)
                    {
                        if (typeof(ICollection).IsAssignableFrom(item.PropertyType))
                        {
                            ICollection vals = (ICollection)value;
                            StringBuilder builderResult = new StringBuilder();
                            foreach (object val in vals)
                            {
                                if (options.ContainsKey(val.ToString()))
                                {
                                    builderResult.AppendFormat("{0},", options[val.ToString()]);
                                }
                            }
                            value = builderResult.ToString().Trim(',');
                        }
                        else if (options.ContainsKey(value.ToString()))
                        {
                            value = options[value.ToString()];
                        }
                    }
                }
                else if ((attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.Input ||
                    attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.MutiLineTextBox ||
                    attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.Hidden) && value != null)
                {

                    if (attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.Input &&
                        (item.PropertyType.Name == "DateTime" || (item.PropertyType.IsGenericType && item.PropertyType.GetGenericArguments()[0].Name == "DateTime")))
                    {
                        string dateFormat = ((TextBoxDescriptor)attribute.MetaData.ViewPortDescriptors[item.Name]).DateFormat;
                        DateTime dateTime = Convert.ToDateTime(value);
                        value = dateTime.ToString(dateFormat);
                    }
                    else
                    {
                        string val = value.ToString().NoHTML().HtmlEncode();
                        if (val.Length > 50)
                        {
                            val = val.Substring(0, 50) + "...";
                        }
                        value = val;
                    }
                    if (attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.Input)
                    {
                        string valueFormat = ((TextBoxDescriptor)attribute.MetaData.ViewPortDescriptors[item.Name]).ValueFormat;
                        if (valueFormat.IsNotNullAndWhiteSpace())
                        {
                            value = string.Format("{{0:{0}}}", valueFormat).FormatWith(value);
                        }
                    }
                }
                else if (attribute.MetaData.ViewPortDescriptors[item.Name].TagType == HTMLEnumerate.HTMLTagTypes.PassWord)
                {
                    value = "******";
                }
                if (columnBuilder.Length > 1)
                {
                    columnBuilder.Append(",");
                }
                columnBuilder.AppendFormat("{0}:\"{1}\"", item.Name, (value ?? ""));
            }
            columnBuilder.Append("},");
            return columnBuilder.ToString();
        }
        private string GetHtmlModelString(BaseDescriptor tag, string dataType)
        {
            int hidden = 0;
            string data = "";
            string dateFormat = "";
            string jsDateFormat = "";
            string valueFormat = "";
            switch (tag.TagType)
            {
                case HTMLEnumerate.HTMLTagTypes.Input:
                    {
                        var textBoxHtmlTag = tag as TextBoxDescriptor;
                        if (textBoxHtmlTag != null)
                        {
                            dateFormat = textBoxHtmlTag.DateFormat;
                            jsDateFormat = textBoxHtmlTag.JavaScriptDateFormat;
                            valueFormat = textBoxHtmlTag.ValueFormat;
                        }
                        break;
                    }
                case HTMLEnumerate.HTMLTagTypes.DropDownList:
                    {
                        var dropTag = (DropDownListDescriptor)tag;
                        if (dropTag.SourceType == SourceType.ViewData && DropDownOptions.ContainsKey(tag.Name))
                        {
                            dropTag.DataSource(DropDownOptions[tag.Name]);
                        }
                        data = dropTag.GetOptions();
                        dataType = "Select";
                        break;
                    }
                case HTMLEnumerate.HTMLTagTypes.MutiSelect:
                    {
                        var muliTag = (MutiSelectDescriptor)tag;
                        if (muliTag.SourceType == SourceType.ViewData && DropDownOptions.ContainsKey(tag.Name))
                        {
                            muliTag.DataSource(DropDownOptions[tag.Name]);
                        }
                        data = muliTag.GetOptions();
                        dataType = "Select";
                        break;
                    }
                case HTMLEnumerate.HTMLTagTypes.File: dataType = "None";
                    break;
                case HTMLEnumerate.HTMLTagTypes.PassWord: dataType = "None";
                    break;
                case HTMLEnumerate.HTMLTagTypes.Hidden: hidden = 1; break;
                default:
                    break;
            }
            if (!tag.GridSetting.Searchable)
            {
                dataType = "None";
            }
            if (!tag.GridSetting.Visiable)
            {
                hidden = 1;
            }
            string displayName = string.IsNullOrEmpty(tag.DisplayName) ? tag.Name : tag.DisplayName;
            StringBuilder columnBuilder = new StringBuilder();
            columnBuilder.AppendFormat("{0}:{{", tag.Name);
            columnBuilder.AppendFormat("DisplayName:'{0}',", displayName.Replace("\"", "\\\"").Replace("\'", "\\\'"));
            columnBuilder.AppendFormat("Name:'{0}',", tag.Name);
            columnBuilder.AppendFormat("Width:{0},", tag.GridSetting.ColumnWidth);
            columnBuilder.AppendFormat("DataType:'{0}',", dataType);
            columnBuilder.AppendFormat("DateFormat:'{0}',", dateFormat);
            columnBuilder.AppendFormat("JsDateFormat:'{0}',", jsDateFormat);
            columnBuilder.AppendFormat("ValueFormat:'{0}',", valueFormat);
            columnBuilder.AppendFormat("Hidden:{0},", hidden);
            columnBuilder.AppendFormat("Data:\"{0}\"", data);
            columnBuilder.Append("},");
            return columnBuilder.ToString();
        }
        private int GetConditionIndex(string conditionKey)
        {
            int i = -1;
            ConditionIndexRegex.Replace(conditionKey, match =>
            {
                if (match.Groups.Count == 2)
                {
                    i = int.Parse(match.Groups[1].Value);
                }
                return null;
            });
            return i;
        }
        private int GetConditionGroupIndex(string conditionGroupKey)
        {
            int i = -1;
            GroupIndexRegex.Replace(conditionGroupKey, match =>
            {
                if (match.Groups.Count == 2)
                {
                    i = int.Parse(match.Groups[1].Value);
                }
                return null;
            });
            return i;
        }
        private int GetGroupConditionIndex(string conditionGroupKey)
        {
            int i = -1;
            GroupConditoinIndexRegex.Replace(conditionGroupKey, match =>
            {
                if (match.Groups.Count == 2)
                {
                    i = int.Parse(match.Groups[1].Value);
                }
                return null;
            });
            return i;
        }

        private string GetConditionProperty(string formKey)
        {
            string property = null;
            PropertyRegex.Replace(formKey, match =>
            {
                if (match.Groups.Count == 2)
                {
                    property = match.Groups[1].Value;
                }
                return null;
            });
            return property;
        }
        #endregion
        public string GetJsonDataForGrid<T>(IEnumerable<T> data, long pageSize, long pageIndex, long recordCount)
        {
            string baseStr = "{{ Columns: {0}, Rows: [{1}] ,PageIndex:{2},PageSize:{3},RecordCount:{4}}}";
            StringBuilder rowBuilder = new StringBuilder();
            DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute<T>();
            foreach (var item in data)
            {
                rowBuilder.Append(GetModelString(item, attribute));
            }
            return string.Format(baseStr, GetHtmlModelString<T>(), rowBuilder.ToString().Trim(','), pageIndex, pageSize, recordCount);
        }
        public string GetJsonDataForGrid<T>(IEnumerable<T> data, Pagination page)
        {
            return this.GetJsonDataForGrid<T>(data, page.PageSize, page.PageIndex, page.RecordCount);
        }
        public string GetHtmlModelString<T>()
        {
            DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute<T>();
            StringBuilder columnBuilder = new StringBuilder();
            if (attribute != null)
            {
                attribute.InitDisplayName();
                foreach (var item in attribute.GetViewPortDescriptors(true))
                {
                    columnBuilder.Append(GetHtmlModelString(item, item.DataType.Name));
                }
            }
            else
            {
                Type tType = typeof(T);
                PropertyInfo[] propertys = tType.GetProperties();
                foreach (var item in propertys)
                {
                    string typeName = item.PropertyType.IsGenericType ? item.PropertyType.GetGenericArguments()[0].Name : item.PropertyType.Name;
                    columnBuilder.AppendFormat("{0}:{{ Name: '{0}',DisplayName:'{0}',Width:150,DataType:'{1}',DateFormat:'',JsDateFormat:'',ValueDateFormat:'',Hidden:0 }},", item.Name, typeName);
                }
            }
            return string.Format("{{{0}}}", columnBuilder.ToString().Trim(','));
        }
        public DataFilter GetDataFilter<T>()
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            DataFilter filter = new DataFilter();
            List<Condition> conditions = new List<Condition>();
            List<ConditionGroup> groups = new List<ConditionGroup>();
            _propertyInfos = typeof(T).GetProperties();
            foreach (var item in _form.AllKeys)
            {
                string property = GetConditionProperty(item);
                if (property.IsNullOrEmpty()) continue;

                #region 单个件

                int conditionIndex = GetConditionIndex(item);
                if (conditionIndex >= 0)
                {
                    if (conditionIndex == conditions.Count)
                    {
                        conditions.Add(new Condition());
                    }
                    SetCondition(conditions[conditionIndex], property, _form[item]);
                }

                #endregion

                #region 组合条件

                int groupIndex = GetConditionGroupIndex(item);
                if (groupIndex >= 0)
                {
                    if (groupIndex == groups.Count)
                    {
                        groups.Add(new ConditionGroup());
                    }
                    int groupConditionIndex = GetGroupConditionIndex(item);
                    if (groupConditionIndex >= 0)
                    {
                        if (groupConditionIndex == groups[groupIndex].Conditions.Count)
                        {
                            groups[groupIndex].Conditions.Add(new Condition());
                        }
                        SetCondition(groups[groupIndex].Conditions[groupConditionIndex], property, _form[item]);
                    }
                    else
                    {
                        SetGroup(groups[groupIndex], property, _form[item]);
                    }
                }
                #endregion
            }

            foreach (var con in conditions)
            {
                if (custAttribute != null)
                {
                    con.Property = custAttribute.GetPropertyMapper(con.Property);
                }
                filter.Where(con);
            }

            foreach (var gro in groups)
            {
                if (custAttribute != null)
                {
                    foreach (var con in gro.Conditions)
                    {
                        con.Property = custAttribute.GetPropertyMapper(con.Property);
                    }
                }
                filter.Where(gro);
            }
            for (int i = 0; ; i++)
            {
                string orderp = _form[string.Format("OrderBy[{0}][OrderCol]", i)];
                string orderType = _form[string.Format("OrderBy[{0}][OrderType]", i)];
                if (string.IsNullOrEmpty(orderp) || string.IsNullOrEmpty(orderType))
                    break;
                Order order = new Order { Property = orderp };
                if (custAttribute != null)
                {
                    order.Property = custAttribute.GetPropertyMapper(order.Property);
                }
                order.OrderType = (OrderType)int.Parse(orderType);
                filter.OrderBy(order);
            }
            return filter;
        }

        public Pagination GetPagination()
        {
            Pagination page = new Pagination { PageIndex = 0, PageSize = 20 };
            if (!string.IsNullOrEmpty(_form["PageIndex"]))
            {
                int pageIndex;
                if (Int32.TryParse(_form["PageIndex"], out pageIndex))
                {
                    page.PageIndex = pageIndex;
                }
            }
            if (!string.IsNullOrEmpty(_form["PageSize"]))
            {
                int pageSize;
                if (Int32.TryParse(_form["PageSize"], out pageSize))
                {
                    page.PageSize = pageSize;
                }
            }
            return page;
        }

        private void SetCondition(Condition condition, string property, string value)
        {
            switch (property)
            {
                case "Property":
                    condition.Property = value;
                    break;
                case "Value":
                    condition.Value = ConvertValue(condition.Property, value);
                    break;
                case "OperatorType":
                    condition.OperatorType = (OperatorType)int.Parse(value);
                    break;
                case "ConditionType":
                    condition.ConditionType = (ConditionType)int.Parse(value); ;
                    break;
            }
        }

        private void SetGroup(ConditionGroup group, string property, string value)
        {
            switch (property)
            {
                case "ConditionType":
                    group.ConditionType = (ConditionType)int.Parse(value); ;
                    break;
            }
        }
        private object ConvertValue(string property, string value)
        {
            try
            {
                var propertyType = _propertyInfos.FirstOrDefault(m => m.Name == property);
                if (propertyType != null)
                {
                    Type type = propertyType.PropertyType.IsGenericType
                        ? propertyType.PropertyType.GetGenericArguments()[0]
                        : propertyType.PropertyType;
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(type);
                    return typeConverter.ConvertFrom(value);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return value;
        }
    }
}
