/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Constant;
using Easy.Modules.DataDictionary;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Easy.ViewPort.Validator;
using Microsoft.Practices.ServiceLocation;

namespace Easy.ViewPort.Descriptor
{
    public class DropDownListDescriptor : BaseDescriptor
    {
        private IDictionary<string, string> _data;
        private Func<Dictionary<string, string>> _souceFunc;
        public DropDownListDescriptor(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.DropDownList;
            this.TemplateName = "DropDownList";
        }

        #region Regular

        public DropDownListDescriptor Required(string errorMessage)
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name,
                ErrorMessage = errorMessage
            });
            this.IsRequired = true;
            return this;
        }
        public DropDownListDescriptor Required()
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name
            });
            this.IsRequired = true;
            return this;
        }

        public DropDownListDescriptor SetDisplayName(string name)
        {
            this.DisplayName = name;
            foreach (ValidatorBase item in this.Validator)
            {
                item.DisplayName = name;
            }
            return this;
        }
        public DropDownListDescriptor AddProperty(string property, string value)
        {
            if (this.Properties.ContainsKey(property))
                this.Properties[property] = value;
            else this.Properties.Add(property, value);
            return this;
        }
        public DropDownListDescriptor AddClass(string name)
        {
            if (!this.Classes.Contains(name))
                this.Classes.Add(name);
            return this;
        }
        public DropDownListDescriptor SetColumnWidth(int width)
        {
            this.GridSetting.ColumnWidth = width;
            return this;
        }
        public DropDownListDescriptor SearchAble(bool? cansearch = true)
        {
            bool search = cansearch ?? true;
            this.GridSetting.Searchable = search;
            return this;
        }
        public DropDownListDescriptor ReadOnly()
        {
            if (!this.Properties.ContainsKey("readonly"))
            {
                this.Properties.Add("readonly", "readonly");
            }
            else
            {
                this.Properties["readonly"] = "readonly";
            }
            if (!this.Properties.ContainsKey("unselectable"))
            {
                this.Properties.Add("unselectable", "on");
            }
            else
            {
                this.Properties["unselectable"] = "on";
            }
            this.IsReadOnly = true;
            return this;
        }

        public DropDownListDescriptor AddStyle(string properyt, string value)
        {
            if (this.Styles.ContainsKey(properyt))
            {
                this.Styles[properyt] = value;
            }
            else
            {
                this.Styles.Add(properyt, value);
            }
            return this;
        }
        public DropDownListDescriptor Hide()
        {
            this.IsHidden = true;
            return this;
        }
        public DropDownListDescriptor Ignore()
        {
            this.IsIgnore = true;
            return this;
        }
        public DropDownListDescriptor HideInGrid()
        {
            this.GridSetting.Visiable = false;
            return this;
        }
        public DropDownListDescriptor Order(int index)
        {
            this.OrderIndex = index;
            return this;
        }
        public DropDownListDescriptor ShowForDisplay(bool show)
        {
            this.IsShowForDisplay = show;
            return this;
        }
        public DropDownListDescriptor ShowForEdit(bool show)
        {
            this.IsShowForEdit = show;
            return this;
        }
        public DropDownListDescriptor SetTemplate(string template)
        {
            this.TemplateName = template;
            return this;
        }

        #endregion
        public IDictionary<string, string> OptionItems
        {
            get
            {
                if (_souceFunc != null)
                {
                    _data = _souceFunc.Invoke();
                }
                return _data ?? (_data = new Dictionary<string, string>());
            }
        }
        public SourceType SourceType { get; set; }
        public string SourceKey { get; set; }
        public string GetOptions()
        {
            var builder = new StringBuilder();
            foreach (var item in OptionItems)
            {
                builder.AppendFormat("<option value='{0}'>{1}</option>", item.Key, item.Value);
            }
            return builder.ToString();
        }
        
        public DropDownListDescriptor DataSource(string Url)
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
        
        public DropDownListDescriptor DataSource(IDictionary<string, string> Data)
        {
            this._data = Data;
            return this;
        }
        
        public DropDownListDescriptor DataSource<T>()
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
        
        public DropDownListDescriptor DataSource(string dictionaryType, SourceType sourceType)
        {
            this.SourceKey = dictionaryType;
            this.SourceType = sourceType;
            if (sourceType == SourceType.Dictionary)
            {
                if (ServiceLocator.IsLocationProviderSet)
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
            }
            return this;
        }
        
        public DropDownListDescriptor DataSource(SourceType type)
        {
            string dictionaryType = this.ModelType.Name + "@" + this.Name;
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

        public DropDownListDescriptor DataSource(Func<Dictionary<string, string>> souceFunc)
        {
            _souceFunc = souceFunc;
            return this;
        }
    }
}
