using Easy.HTML.Grid;
using Easy.HTML.Validator;
using Easy.Extend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public abstract class HtmlTagBase
    {

        public HtmlTagBase(Type modelType, string property)
        {
            Validator = new List<ValidatorBase>();
            this.ModelType = modelType;
            this.Name = property;
            this.DisplayName = this.Name;
            this.OrderIndex = 100;
            this.AddClass("form-control");
        }
        #region Private
        Dictionary<string, string> _Properties = new Dictionary<string, string>();
        Dictionary<string, string> _Styles = new Dictionary<string, string>();
        List<string> _Classes = new List<string>();
        GridSetting _GridSetting = new GridSetting();
        protected string StartStr = "<input type=\"text\"";
        protected string EndStr = "/>";
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type ModelType { get; private set; }
        string errorMsgPlace = "<span class=\"field-validation-valid\" data-valmsg-for=\"{0}\" data-valmsg-replace=\"true\"></span>";
        #endregion
        #region 可继承私有方法
        public virtual void SetValue(object val)
        {
            this.Value = val;
        }
        #endregion
        #region 公共属性
        /// <summary>
        /// 标签类型
        /// </summary>
        public HTMLEnumerate.HTMLTagTypes TagType { get; set; }
        /// <summary>
        /// CSS样式Class集合
        /// </summary>
        public List<string> Classes
        {
            get { return _Classes; }
            private set { _Classes = value; }
        }
        /// <summary>
        /// 标签属性集合
        /// </summary>
        public Dictionary<string, string> Properties
        {
            get { return _Properties; }
            private set { _Properties = value; }
        }
        /// <summary>
        /// 样式Style
        /// </summary>
        public Dictionary<string, string> Styles
        {
            get { return _Styles; }
            private set { _Styles = value; }
        }
        /// <summary>
        /// 名称ID
        /// </summary>
        public string Name { get; private set; }

        public string NamePreFix { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        public object Value { get; set; }

        public object DefaultValue { get; set; }

        /// <summary>
        /// 在列表显示时的配置
        /// </summary>
        public GridSetting Grid
        {
            get { return _GridSetting; }
            set { _GridSetting = value; }
        }
        /// <summary>
        /// 排序索引
        /// </summary>
        public int OrderIndex { get; set; }
        /// <summary>
        /// 数据类型
        /// </summary>
        public Type DataType { get; set; }
        /// <summary>
        /// 验证信息
        /// </summary>
        public List<ValidatorBase> Validator { get; set; }
        /// <summary>
        /// 格式化
        /// </summary>
        public string ValueFormat { get; set; }
        /// <summary>
        /// 只读
        /// </summary>
        public bool IsReadOnly { get; set; }
        /// <summary>
        /// 必填
        /// </summary>
        public bool IsRequired { get; set; }
        /// <summary>
        /// 是否在只读视图中显示
        /// </summary>
        public bool IsShowForDisplay { get; set; }
        public bool IsShowForEdit { get; set; }
        public bool IsIgnore { get; set; }

        public bool IsHidden { get; set; }
        /// <summary>
        /// 显示模板
        /// </summary>
        public string TemplateName { get; set; }
        #endregion

        private string ToHtmlString(bool widthLabel)
        {
            if (string.IsNullOrEmpty(this.DisplayName))
            {
                this.DisplayName = this.Name;
            }
            string val = this.Value == null ? "" : this.Value.ToString().HtmlEncode();
            StringBuilder builder = new StringBuilder();
            if (widthLabel && this.TagType != HTMLEnumerate.HTMLTagTypes.Hidden && !this.IsHidden && !this.IsIgnore)
            {
                builder.AppendFormat("<span class=\"input-group-addon {1}\">{0}</span>", this.DisplayName, this.IsRequired ? "required" : "");
            }
            if (this.TagType == HTMLEnumerate.HTMLTagTypes.File)
            {
                builder.AppendFormat("<input type=\"hidden\" name=\"{0}{1}\" id=\"{0}{1}\" value=\"{2}\" />", this.NamePreFix, this.Name, val);
            }
            builder.Append(StartStr);

            switch (this.TagType)
            {
                case HTMLEnumerate.HTMLTagTypes.PassWord:
                case HTMLEnumerate.HTMLTagTypes.Input:
                case HTMLEnumerate.HTMLTagTypes.Hidden: builder.AppendFormat(" value=\"{0}\" ", val); break;
                case HTMLEnumerate.HTMLTagTypes.CheckBox:
                    {
                        bool check = false;
                        if (val == "")
                            check = false;
                        else check = Convert.ToBoolean(val);
                        builder.AppendFormat(" {0} ", check ? "checked=\"checked\"" : "");
                        builder.Append(" value=\"true\" ");
                        break;
                    }
            }
            builder.AppendFormat(" id=\"{1}{0}\" name=\"{1}{0}\"", this.Name, this.NamePreFix);
            if (Validator.Count > 0)
            {
                this.AddProperty("data-val", "true");
            }
            foreach (ValidatorBase item in Validator)
            {
                item.DisplayName = this.DisplayName;
                if (item is RequiredValidator)
                {
                    this.AddProperty("data-val-required", item.ErrorMessage);
                    this.AddClass("required");
                }
                else if (item is RegularValidator)
                {
                    this.AddProperty("data-val-regex", item.ErrorMessage);
                    this.AddProperty("data-val-regex-pattern", (item as RegularValidator).Expression);
                }
                else if (item is RemoteValidator)
                {
                    RemoteValidator temp = item as RemoteValidator;
                    this.AddProperty("data-val-remote", temp.ErrorMessage);
                    this.AddProperty("data-val-remote-additionalfields", temp.AdditionalFields);
                    this.AddProperty("data-val-remote-type", "");
                    this.AddProperty("data-val-remote-url", temp.Url);
                }
                else if (item is RangeValidator)
                {
                    RangeValidator temp = item as RangeValidator;
                    this.AddProperty("data-val-range", temp.ErrorMessage);
                    this.AddProperty("data-val-range-max", temp.Max.ToString());
                    this.AddProperty("data-val-range-min", temp.Min.ToString());
                }
                else if (item is StringLengthValidator)
                {
                    StringLengthValidator temp = item as StringLengthValidator;
                    this.AddProperty("data-val-length", temp.ErrorMessage);
                    this.AddProperty("data-val-length-max", temp.Max.ToString());
                }
            }
            foreach (var item in this.Properties)
            {
                builder.AppendFormat(" {0}=\"{1}\"", item.Key, item.Value);
            }
            if (Styles.Count > 0)
            {
                builder.Append(" style=\"");
                foreach (var item in Styles)
                {
                    builder.AppendFormat("{0}:{1};", item.Key, item.Value);
                }
                builder.Append("\"");
            }
            if (Classes.Count > 0)
            {
                builder.Append(" class=\"");
                int i = 0;
                foreach (var item in Classes)
                {
                    if (i == 0)
                        builder.AppendFormat("{0}", item);
                    else builder.AppendFormat(" {0}", item);
                    i++;
                }
                builder.Append("\"");
            }
            builder.Append(EndStr);
            if (!this.IsHidden && this.TagType != HTMLEnumerate.HTMLTagTypes.Hidden)
            {
                builder.AppendFormat(errorMsgPlace, this.Name);
            }
            return builder.ToString();
        }

        public override string ToString()
        {
            return this.ToHtmlString(false);
        }
        public virtual string ToString(bool widthLabel)
        {
            return this.ToHtmlString(widthLabel);
        }

        public virtual void ResetValue()
        {
            this.Value = this.DefaultValue == null ? ((this.DataType.IsClass || this.DataType.IsInterface || this.DataType.IsAbstract) ? null : Activator.CreateInstance(this.DataType)) : this.DefaultValue;
        }

        #region

        /// <summary>
        /// 必填项的错误消息
        /// </summary>
        /// <param name="ErrorMessage"></param>
        /// <returns></returns>
        public virtual HtmlTagBase Required(string ErrorMessage)
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name,
                ErrorMessage = ErrorMessage
            });
            this.IsRequired = true;
            return this;
        }
        /// <summary>
        /// 必填
        /// </summary>
        /// <returns></returns>
        public virtual HtmlTagBase Required()
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name
            });
            this.IsRequired = true;
            return this;
        }
        /// <summary>
        /// 最大长度值
        /// </summary>
        /// <param name="Max"></param>
        /// <returns></returns>
        public virtual HtmlTagBase MaxLength(int Max)
        {
            this.Validator.Add(new StringLengthValidator(0, Max)
            {
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 最大长度值
        /// </summary>
        /// <param name="Max"></param>
        /// <returns></returns>
        public virtual HtmlTagBase MaxLength(int Max, string errorMsg)
        {
            this.Validator.Add(new StringLengthValidator(0, Max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        public virtual HtmlTagBase MaxLength(int Min, int Max)
        {
            this.Validator.Add(new StringLengthValidator(Min, Max)
            {
                Property = this.Name
            });
            return this;
        }
        public virtual HtmlTagBase MaxLength(int Min, int Max, string errorMsg)
        {
            this.Validator.Add(new StringLengthValidator(Min, Max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 显示名称
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public virtual HtmlTagBase SetDisplayName(string Value)
        {
            this.DisplayName = Value;
            foreach (ValidatorBase item in this.Validator)
            {
                item.DisplayName = Value;
            }
            return this;
        }
        /// <summary>
        /// 添加属性
        /// </summary>
        /// <param name="Property"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public virtual HtmlTagBase AddProperty(string Property, string Value)
        {
            if (this.Properties.ContainsKey(Property))
                this.Properties[Property] = Value;
            else this.Properties.Add(Property, Value);
            return this;
        }
        /// <summary>
        /// 添加样式
        /// </summary>
        /// <param name="Css"></param>
        /// <returns></returns>
        public virtual HtmlTagBase AddClass(string Css)
        {
            if (!this.Classes.Contains(Css))
                this.Classes.Add(Css);
            return this;
        }
        /// <summary>
        /// 设置列表宽
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public virtual HtmlTagBase SetColumnWidth(int width)
        {
            this.Grid.ColumnWidth = width;
            return this;
        }
        /// <summary>
        /// 是否可搜索
        /// </summary>
        /// <param name="cansearch"></param>
        /// <returns></returns>
        public virtual HtmlTagBase SearchAble(bool? cansearch = true)
        {
            bool search = cansearch ?? true;
            this.Grid.Searchable = search;
            return this;
        }
        /// <summary>
        /// 只读
        /// </summary>
        /// <returns></returns>
        public virtual HtmlTagBase ReadOnly()
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
        /// <summary>
        /// 关闭，无效
        /// </summary>
        /// <returns></returns>
        public virtual HtmlTagBase Disable()
        {
            if (!this.Properties.ContainsKey("disabled"))
            {
                this.Properties.Add("disabled", "disabled");
            }
            else
            {
                this.Properties["disabled"] = "disabled";
            }
            return this;
        }
        /// <summary>
        /// 添加Style样式
        /// </summary>
        /// <param name="properyt">例:margin</param>
        /// <param name="value">例：20px</param>
        /// <returns></returns>
        public virtual HtmlTagBase AddStyle(string properyt, string value)
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
        /// <summary>
        /// 隐藏；display:none
        /// </summary>
        /// <returns></returns>
        public virtual HtmlTagBase Hide()
        {
            this.IsHidden = true;
            return this.AddStyle("display", "none");
        }
        /// <summary>
        /// 不在页面中出现
        /// </summary>
        /// <returns></returns>
        public virtual HtmlTagBase Ignore()
        {
            this.IsIgnore = true;
            return this;
        }
        /// <summary>
        /// 不在列表中显示
        /// </summary>
        /// <returns></returns>
        public virtual HtmlTagBase HideInGrid()
        {
            this.Grid.Visiable = false;
            return this;
        }
        /// <summary>
        /// 排序号
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public virtual HtmlTagBase Order(int index)
        {
            this.OrderIndex = index;
            return this;
        }


        /// <summary>
        /// 正则验证
        /// </summary>
        /// <param name="expression">表达试</param>
        /// <returns></returns>
        public virtual HtmlTagBase RegularExpression(string expression)
        {
            this.Validator.Add(new RegularValidator(expression)
            {
                Property = this.Name
            });
            return this;
        }

        /// <summary>
        /// 正则验证
        /// </summary>
        /// <param name="expression">表达试</param>
        /// <param name="errorMsg">错误提示语</param>
        /// <returns></returns>
        public virtual HtmlTagBase RegularExpression(string expression, string errorMsg)
        {
            this.Validator.Add(new RegularValidator(expression)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 数值区间验证
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <returns></returns>
        public virtual HtmlTagBase Range(double min, double max)
        {
            this.Validator.Add(new RangeValidator(min, max)
            {
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 数值区间验证
        /// </summary>
        /// <param name="min">最小值</param>
        /// <param name="max">最大值</param>
        /// <param name="errorMsg">错误提示语</param>
        /// <returns></returns>
        public virtual HtmlTagBase Range(double min, double max, string errorMsg)
        {
            this.Validator.Add(new RangeValidator(min, max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 远程验证
        /// </summary>
        /// <param name="routeName">路由名称</param>
        /// <param name="errorMsg">错误提示语</param>
        /// <returns></returns>
        public virtual HtmlTagBase Remote(string action, string controller)
        {
            this.Validator.Add(new RemoteValidator()
            {
                Action = action,
                Controller = controller,
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 远程验证
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public virtual HtmlTagBase Remote(string action, string controller, string errorMsg)
        {
            this.Validator.Add(new RemoteValidator()
            {
                ErrorMessage = errorMsg,
                Action = action,
                Controller = controller,
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 远程验证
        /// </summary>
        /// <param name="action"></param>
        /// <param name="controller"></param>
        /// <param name="area"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public virtual HtmlTagBase Remote(string action, string controller, string area, string errorMsg)
        {
            this.Validator.Add(new RemoteValidator()
            {
                ErrorMessage = errorMsg,
                Action = action,
                Controller = controller,
                Area = area,
                Property = this.Name
            });
            return this;
        }
        /// <summary>
        /// 远程验证
        /// </summary>
        /// <param name="validator"></param>
        /// <returns></returns>
        public virtual HtmlTagBase Remote(RemoteValidator validator)
        {
            this.Validator.Add(validator);
            return this;
        }

        public virtual HtmlTagBase ShowForDisplay(bool show)
        {
            this.IsShowForDisplay = show;
            return this;
        }
        public virtual HtmlTagBase ShowForEdit(bool show)
        {
            this.IsShowForEdit = show;
            return this;
        }
        public virtual HtmlTagBase SetTemplate(string template)
        {
            this.TemplateName = template;
            return this;
        }

        public virtual HtmlTagBase Format(string format)
        {
            this.ValueFormat = format;
            return this;
        }

        #endregion
    }
}
