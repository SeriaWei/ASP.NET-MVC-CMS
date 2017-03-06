/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Constant;
using Easy.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;

namespace Easy.ViewPort.Grid
{
    public class EasyGrid<T> where T : class
    {
        public class GridColumn
        {
            Dictionary<string, string> _templates = new Dictionary<string, string>();
            public Dictionary<string, string> Templates
            {
                get { return _templates; }
            }
            public void Add(Expression<Func<T, object>> expression, string TemplateString)
            {
                string key = Reflection.LinqExpression.GetPropertyName(expression.Body);
                TemplateString = TemplateString.Replace("\"", "\\\"");
                if (_templates.ContainsKey(key))
                {
                    _templates[key] = TemplateString;
                }
                else
                {
                    _templates.Add(key, TemplateString);
                }
            }
        }
        private string url;
        private string parent;
        private string onrowBind;
        private bool showCheckbox;
        private string checkBoxCol;
        private string oncheckBoxChange;
        private string onSuccess;
        private bool canSearch;
        private int height;
        string deleteUrl;
        GridColumn columnsTemplate = new GridColumn();
        Dictionary<string, int> orderBy = new Dictionary<string, int>();
        Dictionary<string, Dictionary<string, string>> _options;
        public Dictionary<string, Dictionary<string, string>> DropDownOptions
        {
            get { return _options ?? (_options = new Dictionary<string, Dictionary<string, string>>()); }
            set { _options = value; }
        }
        private string toolBarSelector;
        /// <summary>
        /// 容器名称
        /// </summary>
        /// <param name="Parent"></param>
        public EasyGrid(string Parent)
        {
            this.SearchAble();
            this.parent = Parent;
        }
        public EasyGrid()
        {
            this.SearchAble();
            this.parent = Guid.NewGuid().ToString().Replace("-", "");
        }
        /// <summary>
        /// 列表名称
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public EasyGrid<T> Name(string name)
        {
            this.parent = name;
            return this;
        }
        /// <summary>
        /// 行绑定是触发事件function(model){}
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public EasyGrid<T> OnRowBinding(string function)
        {
            onrowBind = function;
            return this;
        }
        /// <summary>
        /// 选中时触发事件function(selValue){}
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public EasyGrid<T> OnCheckBoxChange(string function)
        {
            oncheckBoxChange = function;
            return this;
        }
        /// <summary>
        /// 数据绑定完成
        /// </summary>
        /// <param name="function"></param>
        /// <returns></returns>
        public EasyGrid<T> OnSuccess(string function)
        {
            onSuccess = function;
            return this;
        }
        /// <summary>
        /// 获取数据的URL
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public virtual EasyGrid<T> DataSource(string url)
        {
            this.url = url;
            return this;
        }
        /// <summary>
        /// 设置列显示模板
        /// </summary>
        /// <param name="columns"></param>
        /// <returns></returns>
        public EasyGrid<T> SetColumnTemplate(Action<GridColumn> columns)
        {
            columns(columnsTemplate);
            return this;
        }
        /// <summary>
        /// 显示Checkbox
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public EasyGrid<T> ShowCheckbox(Expression<Func<T, object>> expression)
        {
            showCheckbox = true;
            checkBoxCol = Reflection.LinqExpression.GetPropertyName(expression.Body);
            return this;
        }
        /// <summary>
        /// 可搜索
        /// </summary>
        /// <param name="isCanSearch"></param>
        /// <returns></returns>
        public EasyGrid<T> SearchAble(bool? isCanSearch = true)
        {
            canSearch = isCanSearch ?? true;
            return this;
        }
        /// <summary>
        /// 设置列表高度
        /// </summary>
        /// <param name="height"></param>
        /// <returns></returns>
        public EasyGrid<T> Height(int height)
        {
            this.height = height;
            return this;
        }
        /// <summary>
        /// 默认排序
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public EasyGrid<T> OrderBy(Expression<Func<T, object>> expression, OrderType orderType)
        {
            string property = Reflection.LinqExpression.GetPropertyName(expression.Body);
            if (orderBy.ContainsKey(property))
            {
                orderBy[property] = (int)orderType;
            }
            else
            {
                orderBy.Add(property, (int)orderType);
            }
            return this;
        }
        /// <summary>
        /// 将selector设置为列表工具栏
        /// </summary>
        /// <param name="selector"></param>
        /// <returns></returns>
        public EasyGrid<T> SetAsToolBar(string selector)
        {
            this.toolBarSelector = selector;
            return this;
        }
        public virtual EasyGrid<T> DeleteUrl(string url)
        {
            this.deleteUrl = url;
            return this;
        }
        /// <summary>
        /// 结束配置
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat("<div id='{0}'></div>", this.parent);
            builder.Append("<script type='text/javascript'>$(function(){");
            builder.Append("Easy.Grid()");
            builder.AppendFormat(".SetGridArea('{0}')", parent);
            builder.AppendFormat(".SetUrl('{0}')", url);
            foreach (var item in columnsTemplate.Templates)
            {
                builder.AppendFormat(".SetColumnTemplete('{0}', \"{1}\")", item.Key, item.Value);
            }
            if (!string.IsNullOrEmpty(onrowBind))
            {
                builder.AppendFormat(".OnRowDataBind({0})", onrowBind);
            }
            if (!string.IsNullOrEmpty(oncheckBoxChange))
            {
                builder.AppendFormat(".OnCheckBoxChange({0})", oncheckBoxChange);
            }
            if (!string.IsNullOrEmpty(onSuccess))
            {
                builder.AppendFormat(".OnSuccess({0})", onSuccess);
            }
            if (showCheckbox)
            {
                builder.AppendFormat(".ShowCheckBox('{0}')", checkBoxCol);
            }
            if (height > 0)
            {
                builder.AppendFormat(".Height('{0}')", height);
            }
            if (canSearch)
            {
                builder.Append(".SearchAble()");
            }
            if (orderBy.Count > 0)
            {
                foreach (var item in orderBy.Keys)
                {
                    builder.AppendFormat(".OrderBy('{0}',{1})", item, orderBy[item]);
                }
            }
            if (!string.IsNullOrEmpty(this.toolBarSelector))
            {
                builder.AppendFormat(".SetBoolBar('{0}')", this.toolBarSelector);
            }
            if (!string.IsNullOrEmpty(this.deleteUrl))
            {
                builder.AppendFormat(".SetDeleteUrl('{0}')", this.deleteUrl);
            }
            var grid = new GridData(null) { DropDownOptions = DropDownOptions };
            builder.AppendFormat(".SetModel({0})", grid.GetHtmlModelString<T>());
            builder.Append(".Show();});</script>");

            return builder.ToString();
        }
    }
}
