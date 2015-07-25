using Easy.Constant;
using Easy.Modules.DataDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.HTML.Tags
{
    public class DropDownListHtmlTag : HtmlTagBase
    {
        private Dictionary<string, string> _data;
        private Func<Dictionary<string, string>> _souceFunc;
        public DropDownListHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.DropDownList;
            this.StartStr = "<select";
            this.EndStr = "></select>";
        }
        public Dictionary<string, string> OptionItems
        {
            get
            {
                if (_souceFunc != null && _data == null)
                {
                    _data = _souceFunc.Invoke();
                }
                return _data ?? (_data = new Dictionary<string, string>());
            }
        }
        public SourceType SourceType { get; set; }
        public string SourceKey { get; set; }
        public override string ToString()
        {
            return ToString(false);
        }

        public override string ToString(bool widthLabel)
        {
            string baseStr = base.ToString(widthLabel);
            var builder = new StringBuilder();
            string val = this.Value == null ? "" : this.Value.ToString();
            if (this.Value != null && this.Value is ICollection)
            {
                var vals = this.Value as ICollection;

                foreach (var item in OptionItems)
                {
                    bool selected = false;
                    foreach (object valItem in vals)
                    {
                        if (valItem.ToString() == item.Key)
                            selected = true;
                    }
                    builder = AppendOption(builder, item.Key, item.Value, selected);
                }

            }
            else
            {
                builder = OptionItems.Aggregate(builder, (current, item) => AppendOption(current, item.Key, item.Value, item.Key == val));
            }
            return baseStr.Replace("</select>", builder.ToString() + "</select>");
        }

        private StringBuilder AppendOption(StringBuilder builder, string key, string text, bool selected)
        {
            if (selected)
            {
                builder.AppendFormat("<option value='{0}' selected='selected'>{1}</option>", key, text);
            }
            else
            {
                builder.AppendFormat("<option value='{0}'>{1}</option>", key, text);
            }
            return builder;
        }

        public string GetOptions()
        {
            var builder = new StringBuilder();
            foreach (var item in OptionItems)
            {
                builder.AppendFormat("<option value='{0}'>{1}</option>", item.Key, item.Value);
            }
            return builder.ToString();
        }
        /// <summary>
        /// 异步请求数据源
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public DropDownListHtmlTag DataSource(string Url)
        {
            if (this.Properties.ContainsKey("DataSource"))
            {
                this.Properties["DataSource"] = Url;
            }
            else
            {
                this.Properties.Add("DataSource", Url);
            }
            return this;
        }
        /// <summary>
        /// 数据字典数据源
        /// </summary>
        /// <param name="Data"></param>
        /// <returns></returns>
        public DropDownListHtmlTag DataSource(Dictionary<string, string> Data)
        {
            this._data = Data;
            return this;
        }
        /// <summary>
        /// 枚举数据源
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public DropDownListHtmlTag DataSource<T>()
        {
            Type dataType = typeof(T);
            if (!dataType.IsEnum)
            {
                throw new Exception(dataType.FullName + ",不是枚举类型。");
            }
            string[] text = Enum.GetNames(dataType);
            Dictionary<string, string> lan = new Dictionary<string, string>();
            foreach (var item in text)
            {
                lan.Add(item, dataType.Name + "|" + item);
            }
            lan = Localization.InitLan(lan);
            for (int i = 0; i < text.Length; i++)
            {
                if (lan.ContainsKey(text[i]))
                {
                    this._data.Add(Enum.Format(dataType, Enum.Parse(dataType, text[i], true), "d"), lan[text[i]]);
                }
                else
                {
                    this._data.Add(Enum.Format(dataType, Enum.Parse(dataType, text[i], true), "d"), text[i]);
                }
            }

            return this;
        }
        /// <summary>
        /// DataDictionary数据源
        /// </summary>
        /// <param name="dictionaryType"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public DropDownListHtmlTag DataSource(string dictionaryType, SourceType sourceType)
        {
            this.SourceKey = dictionaryType;
            this.SourceType = sourceType;
            if (sourceType == SourceType.Dictionary)
            {
                IDataDictionaryService dicService = ServiceLocator.Current.GetInstance<IDataDictionaryService>();
                if (dicService != null)
                {
                    if (this._data == null)
                    {
                        _data = new Dictionary<string, string>();
                    }
                    foreach (DataDictionaryEntity item in dicService.GetDictionaryByType(dictionaryType))
                    {
                        this._data.Add(item.ID.ToString(), item.Title);
                    }
                }
            }
            return this;
        }
        /// <summary>
        /// DataDictionary数据源，类别为EntityName_PropertyName
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public DropDownListHtmlTag DataSource(SourceType type)
        {
            string dictionaryType = this.ModelType.Name + "_" + this.Name;
            if (type == SourceType.Dictionary)
            {
                var dicService = ServiceLocator.Current.GetInstance<IDataDictionaryService>();
                if (dicService != null)
                {
                    if (this._data == null)
                    {
                        _data=new Dictionary<string, string>();
                    }
                    foreach (DataDictionaryEntity item in dicService.GetDictionaryByType(dictionaryType))
                    {
                        this._data.Add(item.DicValue, item.Title);
                    }
                }
            }
            return this;
        }

        public DropDownListHtmlTag DataSource(Func<Dictionary<string, string>> souceFunc)
        {
            _souceFunc = souceFunc;
            return this;
        }
        public DropDownListHtmlTag EffectTo(string ProPerty)
        {
            if (this.Properties.ContainsKey("EffectTo"))
            {
                this.Properties["EffectTo"] = ProPerty;
            }
            else
            {
                this.Properties.Add("EffectTo", ProPerty);
            }
            return this;
        }
        public DropDownListHtmlTag EffectTo<T>(Expression<Func<T, object>> ex)
        {
            this.EffectTo(Common.GetLinqExpressionText(ex));
            return this;
        }


        #region 重写方法
        public new DropDownListHtmlTag AddClass(string Css)
        {
            return base.AddClass(Css) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag AddProperty(string Property, string Value)
        {
            return base.AddProperty(Property, Value) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag AddStyle(string properyt, string value)
        {
            return base.AddStyle(properyt, value) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag Hide()
        {
            return base.Hide() as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag HideInGrid()
        {
            return base.HideInGrid() as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag MaxLength(int Max)
        {
            return base.MaxLength(Max) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag MaxLength(int Max, string errorMsg)
        {
            return base.MaxLength(Max, errorMsg) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag MaxLength(int Min, int Max)
        {
            return base.MaxLength(Min, Max) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag MaxLength(int Min, int Max, string errorMsg)
        {
            return base.MaxLength(Min, Max, errorMsg) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag Order(int index)
        {
            return base.Order(index) as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag Range(double min, double max)
        {
            return base.Range(min, max) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag Range(double min, double max, string errorMsg)
        {
            return base.Range(min, max, errorMsg) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag ReadOnly()
        {
            return base.ReadOnly() as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag RegularExpression(string expression)
        {
            return base.RegularExpression(expression) as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag RegularExpression(string expression, string errorMsg)
        {
            return base.RegularExpression(expression, errorMsg) as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag Remote(string action, string controller)
        {
            return base.Remote(action, controller) as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag Remote(string action, string controller, string area, string errorMsg)
        {
            return base.Remote(action, controller, area, errorMsg) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag Remote(string action, string controller, string errorMsg)
        {
            return base.Remote(action, controller, errorMsg) as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag Required()
        {
            return base.Required() as DropDownListHtmlTag;
        }

        public new DropDownListHtmlTag Required(string ErrorMessage)
        {
            return base.Required(ErrorMessage) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag SearchAble(bool? cansearch = true)
        {
            return base.SearchAble(cansearch) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag SetColumnWidth(int width)
        {
            return base.SetColumnWidth(width) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag SetDisplayName(string Value)
        {
            return base.SetDisplayName(Value) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag ShowForDisplay(bool show)
        {
            return base.ShowForDisplay(show) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag ShowForEdit(bool show)
        {
            return base.ShowForEdit(show) as DropDownListHtmlTag;
        }
        public new DropDownListHtmlTag SetTemplate(string template)
        {
            return base.SetTemplate(template) as DropDownListHtmlTag;
        }

        #endregion

        public override void ResetValue()
        {
            base.ResetValue();
            if (this.SourceType == SourceType.ViewData && this._data != null)
            {
                this._data.Clear();
            }
        }
    }
}
