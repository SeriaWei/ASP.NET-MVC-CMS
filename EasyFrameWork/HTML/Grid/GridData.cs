using Easy.Data;
using Easy.HTML.Tags;
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

namespace Easy.HTML.Grid
{
    public class GridData
    {
        NameValueCollection _form;
        private Func<HtmlTagBase, Dictionary<string, string>> _dataSource;

        Dictionary<string, Dictionary<string, string>> _options = new Dictionary<string, Dictionary<string, string>>();
        public Dictionary<string, Dictionary<string, string>> DropDownOptions
        {
            get { return _options; }
            set { _options = value; }
        }

        public GridData(NameValueCollection form)
        {
            this._form = form;
        }
        public GridData(NameValueCollection form, Func<HtmlTagBase, Dictionary<string, string>> dataSource)
        {
            this._form = form;
            _dataSource = dataSource;
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
                if ((attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.DropDownList ||
                    attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.MutiSelect)
                    && value != null)
                {
                    var tag = attribute.MetaData.HtmlTags[item.Name] as DropDownListHtmlTag;
                    Dictionary<string, string> Data = tag.OptionItems;
                    if (tag.SourceType == SourceType.ViewData)
                    {
                        if (_dataSource != null)
                        {
                            Data = _dataSource(tag);
                        }
                    }
                    if (Data != null)
                    {
                        if (typeof(ICollection).IsAssignableFrom(item.PropertyType))
                        {
                            ICollection vals = value as ICollection;
                            StringBuilder builderResult = new StringBuilder();
                            foreach (object val in vals)
                            {
                                if (Data.ContainsKey(val.ToString()))
                                {
                                    builderResult.AppendFormat("{0},", Data[val.ToString()]);
                                }
                            }
                            value = builderResult.ToString().Trim(',');
                        }
                        else if (Data.ContainsKey(value.ToString()))
                        {
                            value = Data[value.ToString()];
                        }
                    }
                }
                else if ((attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.Input ||
                    attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.MutiLineTextBox ||
                    attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.Hidden) && value != null)
                {

                    if (attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.Input &&
                        (item.PropertyType.Name == "DateTime" || (item.PropertyType.Name == "Nullable`1" && item.PropertyType.GetGenericArguments()[0].Name == "DateTime")))
                    {
                        string dateFormat = (attribute.MetaData.HtmlTags[item.Name] as HTML.Tags.TextBoxHtmlTag).DateFormat;
                        DateTime dateTime = Convert.ToDateTime(value);
                        value = dateTime.ToString(dateFormat);
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(attribute.MetaData.HtmlTags[item.Name].ValueFormat))
                        {
                            value = string.Format("{0:" + attribute.MetaData.HtmlTags[item.Name].ValueFormat + "}", value);
                        }
                        string val = value.ToString().NoHTML().HtmlEncode();
                        if (val.Length > 50)
                        {
                            val = val.Substring(0, 50) + "...";
                        }
                        value = val;
                    }
                }
                else if (attribute.MetaData.HtmlTags[item.Name].TagType == HTML.HTMLEnumerate.HTMLTagTypes.PassWord)
                {
                    value = "******";
                }
                else if (attribute.MetaData.HtmlTags[item.Name].DataType.Name == "Boolean" && value != null)
                {
                    value = Convert.ToBoolean(value) ? "是" : "否";
                }
                if (!string.IsNullOrEmpty(attribute.MetaData.HtmlTags[item.Name].ValueFormat))
                {
                    value = string.Format("{0:" + attribute.MetaData.HtmlTags[item.Name].ValueFormat + "}", value);
                }
                if (columnBuilder.Length == 1)
                {
                    columnBuilder.AppendFormat("{0}:\"{1}\"", item.Name, (value ?? "").ToString());
                }
                else
                {
                    columnBuilder.AppendFormat(",{0}:\"{1}\"", item.Name, (value ?? "").ToString());
                }
            }
            columnBuilder.Append("},");
            return columnBuilder.ToString();
        }
        private string GetHtmlModelString(HtmlTagBase Tag, string DataType)
        {
            int hidden = 0;
            string Data = "";
            string format = "";
            switch (Tag.TagType)
            {
                case Easy.HTML.HTMLEnumerate.HTMLTagTypes.Input:
                    {
                        format = (Tag as TextBoxHtmlTag).DateFormat;
                        break;
                    }
                case Easy.HTML.HTMLEnumerate.HTMLTagTypes.DropDownList:
                    {
                        var dropTag = Tag as DropDownListHtmlTag;
                        if (dropTag.SourceType == SourceType.ViewData && DropDownOptions.ContainsKey(Tag.Name))
                        {
                            dropTag.DataSource(DropDownOptions[Tag.Name]);
                        }
                        Data = dropTag.GetOptions();
                        DataType = "Select";
                        break;
                    }
                case Easy.HTML.HTMLEnumerate.HTMLTagTypes.MutiSelect:
                    {
                        var muliTag = Tag as MutiSelectHtmlTag;
                        if (muliTag.SourceType == SourceType.ViewData && DropDownOptions.ContainsKey(Tag.Name))
                        {
                            muliTag.DataSource(DropDownOptions[Tag.Name]);
                        }
                        Data = muliTag.GetOptions();
                        DataType = "Select";
                        break;
                    }
                case Easy.HTML.HTMLEnumerate.HTMLTagTypes.File: DataType = "None";
                    break;
                case Easy.HTML.HTMLEnumerate.HTMLTagTypes.PassWord: DataType = "None";
                    break;
                case HTML.HTMLEnumerate.HTMLTagTypes.Hidden: hidden = 1; break;
                default:
                    break;
            }
            if (!Tag.Grid.Searchable)
            {
                DataType = "None";
            }
            if (!Tag.Grid.Visiable)
            {
                hidden = 1;
            }
            Tag.DisplayName = string.IsNullOrEmpty(Tag.DisplayName) ? Tag.Name : Tag.DisplayName;
            StringBuilder columnBuilder = new StringBuilder();
            columnBuilder.AppendFormat("{0}:{{", Tag.Name);
            columnBuilder.AppendFormat("DisplayName:'{0}',", Tag.DisplayName.Replace("\"", "\\\"").Replace("\'", "\\\'"));
            columnBuilder.AppendFormat("Name:'{0}',", Tag.Name);
            columnBuilder.AppendFormat("Width:{0},", Tag.Grid.ColumnWidth);
            columnBuilder.AppendFormat("DataType:'{0}',", DataType);
            columnBuilder.AppendFormat("Format:'{0}',", format);
            columnBuilder.AppendFormat("Hidden:{0},", hidden);
            columnBuilder.AppendFormat("Data:\"{0}\"", Data);
            columnBuilder.Append("},");
            return columnBuilder.ToString();
        }
        private int GetConditionIndex(string conditionKey)
        {
            if (conditionKey.Contains("Conditions["))
            {
                conditionKey = conditionKey.Replace("Conditions[", "");
                string index = conditionKey.Substring(0, conditionKey.IndexOf(']'));
                int returnValue = -1;
                return int.TryParse(index, out returnValue) ? returnValue : -1;
            }
            else return -1;
        }
        private int GetConditionGroupIndex(string conditionGroupKey)
        {
            if (conditionGroupKey.Contains("ConditionGroups["))
            {
                conditionGroupKey = conditionGroupKey.Replace("ConditionGroups[", "");
                string index = conditionGroupKey.Substring(0, conditionGroupKey.IndexOf(']'));
                int returnValue = -1;
                return int.TryParse(index, out returnValue) ? returnValue : -1;
            }
            else return -1;
        }
        private int GetGroupConditionIndex(int groupIndex, string conditionGroupKey)
        {
            if (conditionGroupKey.Contains(string.Format("ConditionGroups[{0}][Conditions][", groupIndex)))
            {
                conditionGroupKey = conditionGroupKey.Replace(string.Format("ConditionGroups[{0}][Conditions][", groupIndex), "");
                string index = conditionGroupKey.Substring(0, conditionGroupKey.IndexOf(']'));
                int returnValue = -1;
                return int.TryParse(index, out returnValue) ? returnValue : -1;
            }
            else return -1;
        }
        #endregion
        public string GetJsonDataForGrid<T>(IEnumerable<T> data, long pageSize, long pageIndex, long recordCount)
        {
            string baseStr = "{{ Columns: {0}, Rows: [{1}] ,PageIndex:{2},PageSize:{3},RecordCount:{4}}}";
            StringBuilder RowBuilder = new StringBuilder();
            DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute<T>();
            foreach (var item in data)
            {
                RowBuilder.Append(GetModelString(item, attribute));
            }
            return string.Format(baseStr, GetHtmlModelString<T>(), RowBuilder.ToString().Trim(','), pageIndex, pageSize, recordCount);
        }
        public string GetJsonDataForGrid<T>(IEnumerable<T> data, Pagination page)
        {
            return this.GetJsonDataForGrid<T>(data, page.PageSize, page.PageIndex, page.RecordCount);
        }
        public string GetHtmlModelString<T>()
        {
            DataConfigureAttribute attribute = DataConfigureAttribute.GetAttribute<T>();
            StringBuilder ColumnBuilder = new StringBuilder();
            if (attribute != null)
            {
                foreach (var item in attribute.GetHtmlTags(true))
                {
                    ColumnBuilder.Append(GetHtmlModelString(item, item.DataType.Name));
                }
            }
            else
            {
                Type tType = typeof(T);
                System.Reflection.PropertyInfo[] propertys = tType.GetProperties();
                foreach (var item in propertys)
                {
                    string typeName = item.PropertyType.Name;
                    if (item.PropertyType.Name == "Nullable`1")
                    {
                        typeName = item.PropertyType.GetGenericArguments()[0].Name;
                    }
                    ColumnBuilder.AppendFormat("{0}:{{ Name: '{0}',DisplayName:'{0}',Width:150,DataType:'{1}',Format:'',,Hidden:0 }},", item.Name, typeName);
                }
            }
            return string.Format("{{{0}}}", ColumnBuilder.ToString().Trim(','));
        }
        public DataFilter GetDataFilter<T>()
        {
            DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute<T>();
            DataFilter filter = new DataFilter();
            int conditionIndex = -1;
            int groupIndex = -1;
            int groupConditionIndex = -1;
            ConditionGroup Group = new ConditionGroup();
            foreach (var item in _form.AllKeys)
            {
                #region 单件
                int tempIndex = GetConditionIndex(item);
                if (tempIndex >= 0 && conditionIndex != tempIndex)
                {
                    conditionIndex = tempIndex;
                    string value = _form[string.Format("Conditions[{0}][Value]", tempIndex)];
                    if (!string.IsNullOrEmpty(value))
                    {
                        Condition condition = new Condition();
                        condition.Property = _form[string.Format("Conditions[{0}][Property]", tempIndex)];
                        if (custAttribute != null)
                        {
                            condition.Property = custAttribute.GetPropertyMapper(condition.Property);
                        }
                        condition.OperatorType = (OperatorType)int.Parse(_form[string.Format("Conditions[{0}][OperatorType]", tempIndex)]);
                        condition.ConditionType = (ConditionType)int.Parse(_form[string.Format("Conditions[{0}][ConditionType]", tempIndex)]);
                        condition.Value = ConvertValue(_form[string.Format("Conditions[{0}][DataType]", tempIndex)], value);
                        filter.Where(condition);
                    }
                }
                #endregion
                #region 条件组合
                int tempGroupIndex = GetConditionGroupIndex(item);
                if (tempGroupIndex >= 0)
                {
                    if (tempGroupIndex != groupIndex && groupIndex != -1)
                    {
                        filter.Where(Group);
                        groupConditionIndex = -1;
                        groupIndex = -1;
                        Group = new ConditionGroup();
                    }
                    groupIndex = tempGroupIndex;
                    int tempGroupConditionIndex = GetGroupConditionIndex(tempGroupIndex, item);
                    if (tempGroupConditionIndex >= 0 && groupConditionIndex != tempGroupConditionIndex)
                    {
                        groupConditionIndex = tempGroupConditionIndex;
                        string value = _form[string.Format("ConditionGroups[{0}][Conditions][{1}][Value]", tempGroupIndex, tempGroupConditionIndex)];
                        if (!string.IsNullOrEmpty(value))
                        {
                            Condition condition = new Condition();
                            string proterty = _form[string.Format("ConditionGroups[{0}][Conditions][{1}][Property]", tempGroupIndex, tempGroupConditionIndex)];
                            condition.Property = proterty;
                            if (custAttribute != null)
                            {
                                condition.Property = custAttribute.GetPropertyMapper(condition.Property);
                            }
                            condition.OperatorType = (OperatorType)int.Parse(_form[string.Format("ConditionGroups[{0}][Conditions][{1}][OperatorType]", tempGroupIndex, tempGroupConditionIndex)]);
                            condition.ConditionType = (ConditionType)int.Parse(_form[string.Format("ConditionGroups[{0}][Conditions][{1}][ConditionType]", tempGroupIndex, tempGroupConditionIndex)]);
                            condition.Value = ConvertValue(_form[string.Format("ConditionGroups[{0}][Conditions][{1}][ConditionType]", tempGroupIndex, tempGroupConditionIndex)], value);
                            if (custAttribute != null)
                            {
                                PropertyDataInfo propertyDataInfo = custAttribute.MetaData.PropertyDataConfig[proterty];
                                if (propertyDataInfo.Search != null)
                                {
                                    condition = propertyDataInfo.Search(condition);
                                }
                            }
                            Group.Add(condition);
                        }
                    }
                }
                #endregion
            }
            if (Group.Conditions.Count > 0)
            {
                filter.Where(Group);
            }
            for (int i = 0; ; i++)
            {
                string orderp = _form[string.Format("OrderBy[{0}][OrderCol]", i)];
                string orderType = _form[string.Format("OrderBy[{0}][OrderType]", i)];
                if (string.IsNullOrEmpty(orderp) || string.IsNullOrEmpty(orderType)) break;

                Order order = new Order();
                order.Property = orderp;
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
            Pagination page = new Pagination() { PageIndex = 0, PageSize = 20 };
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
        private object ConvertValue(string TypeStr, string value)
        {
            object result;
            switch (TypeStr)
            {
                case "Boolean": result = Convert.ToBoolean(value); break;
                case "DateTime": result = Convert.ToDateTime(value); break;
                default: result = value; break;
            }
            return result;
        }
    }
}
