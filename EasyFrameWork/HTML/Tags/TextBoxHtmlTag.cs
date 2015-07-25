using Easy.HTML.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public class TextBoxHtmlTag : HtmlTagBase
    {
        public TextBoxHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.Input;
            this.StartStr = "<input";
            this.EndStr = "/>";
            this.AddProperty("type", "text");
        }
        public override void SetValue(object val)
        {
            if (val != null && DataType.Name == "DateTime")
            {
                DateTime time = Convert.ToDateTime(val);
                Value = time.ToString(DateFormat);
            }
            else
            {
                base.SetValue(val);
            }

        }
        public string DateFormat { get; set; }
        /// <summary>
        /// 只显示日期，格式为：年/月/日
        /// </summary>
        /// <returns></returns>
        public TextBoxHtmlTag FormatAsDate()
        {
            this.DateFormat = "yyyy/MM/dd";
            this.AddProperty("DateFormat", this.DateFormat);
            this.AddProperty("ValueType", "Date");
            this.AddClass("Date");
            return this;
        }
        /// <summary>
        /// 显示日期和时间，格式为：年/月/日 时:分
        /// </summary>
        /// <returns></returns>
        public TextBoxHtmlTag FormatAsDateTime()
        {
            this.DateFormat = "yyyy/MM/dd H:mm";
            this.AddProperty("DateFormat", this.DateFormat);
            this.AddProperty("ValueType", "Date");
            this.AddClass("Date");
            return this;
        }
        /// <summary>
        /// 自定义格式化日期类型
        /// </summary>
        /// <param name="format"></param>
        /// <returns></returns>
        public TextBoxHtmlTag FormatDate(string format)
        {
            this.DateFormat = format;
            this.AddProperty("DateFormat", format);
            this.AddProperty("ValueType", "Date");
            this.AddClass("Date");
            return this;
        }

        /// <summary>
        /// 没有文本的时候的提示信息(HTML5)
        /// </summary>
        /// <param name="info">消息内容</param>
        /// <returns></returns>
        public TextBoxHtmlTag PlaceHolder(string info)
        {
            this.AddProperty("PlaceHolder", info);
            return this;
        }
        /// <summary>
        /// 跟据用户输入，自动匹配出相应数据。
        /// </summary>
        /// <param name="dataSource">数据源地址</param>
        /// <returns></returns>
        public TextBoxHtmlTag AutoComplete(string dataSource)
        {
            this.AddProperty("AutoComplete", "true");
            this.AddProperty("DataSource", dataSource);
            this.AddClass("autocomplete");
            return this;
        }
        /// <summary>
        /// 跟据用户输入，自动匹配出相应数据。
        /// </summary>
        /// <param name="dataSource">数据源地址</param>
        /// <param name="valueField">填充值字段</param>
        /// <returns></returns>
        public TextBoxHtmlTag AutoComplete(string dataSource, string valueField)
        {
            this.AddProperty("AutoComplete", "true");
            this.AddProperty("DataSource", dataSource);
            this.AddProperty("ValueField", valueField);
            this.AddClass("autocomplete");
            return this;
        }
        /// <summary>
        ///  跟据用户输入，自动匹配出相应数据。
        /// </summary>
        /// <param name="dataSource">数据源地址</param>
        /// <param name="valueField">填充值字段</param>
        /// <param name="funAddParameter">获取数据源时要提交的额外参数数据的JavaScript方法</param>
        /// <returns></returns>
        public TextBoxHtmlTag AutoComplete(string dataSource, string valueField, string funAddParameter)
        {
            this.AddProperty("AutoComplete", "true");
            this.AddProperty("DataSource", dataSource);
            this.AddProperty("ValueField", valueField);
            this.AddProperty("FunAddParameter", funAddParameter);
            this.AddClass("autocomplete");
            return this;
        }
        /// <summary>
        /// 邮箱格式输入
        /// </summary>
        /// <returns></returns>
        public TextBoxHtmlTag Email()
        {
            this.AddClass("Email");
            this.Validator.Add(new RegularValidator(Constant.RegularExpression.Email)
            {
                Property = this.Name,
                DisplayName = this.DisplayName,
                ErrorMessage = "输入的邮件格式不正确"
            });
            return this;
        }
        /// <summary>
        /// 邮箱格式输入
        /// </summary>
        /// <param name="ErrorMsg">错误消息</param>
        /// <returns></returns>
        public TextBoxHtmlTag Email(string ErrorMsg)
        {
            this.AddProperty("EmailErrorMsg", ErrorMsg);
            this.Validator.Add(new RegularValidator(Constant.RegularExpression.Email)
            {
                ErrorMessage = ErrorMsg,
                Property = this.Name,
                DisplayName = this.DisplayName
            });
            return this.Email();
        }


        #region 重写方法
        public new TextBoxHtmlTag AddClass(string Css)
        {
            return base.AddClass(Css) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag AddProperty(string Property, string Value)
        {
            return base.AddProperty(Property, Value) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag AddStyle(string properyt, string value)
        {
            return base.AddStyle(properyt, value) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag Hide()
        {
            return base.Hide() as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag HideInGrid()
        {
            return base.HideInGrid() as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag MaxLength(int Max)
        {
            return base.MaxLength(Max) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag MaxLength(int Max, string errorMsg)
        {
            return base.MaxLength(Max, errorMsg) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag MaxLength(int Min, int Max)
        {
            return base.MaxLength(Min, Max) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag MaxLength(int Min, int Max, string errorMsg)
        {
            return base.MaxLength(Min, Max, errorMsg) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag Order(int index)
        {
            return base.Order(index) as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag Range(double min, double max)
        {
            return base.Range(min, max) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag Range(double min, double max, string errorMsg)
        {
            return base.Range(min, max, errorMsg) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag ReadOnly()
        {
            return base.ReadOnly() as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag RegularExpression(string expression)
        {
            return base.RegularExpression(expression) as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag RegularExpression(string expression, string errorMsg)
        {
            return base.RegularExpression(expression, errorMsg) as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag Remote(string action, string controller)
        {
            return base.Remote(action, controller) as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag Remote(string action, string controller, string area, string errorMsg)
        {
            return base.Remote(action, controller, area, errorMsg) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag Remote(string action, string controller, string errorMsg)
        {
            return base.Remote(action, controller, errorMsg) as TextBoxHtmlTag;
        }

        public new HtmlTagBase Remote(RemoteValidator validator)
        {
            return base.Remote(validator) as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag Required()
        {
            return base.Required() as TextBoxHtmlTag;
        }

        public new TextBoxHtmlTag Required(string ErrorMessage)
        {
            return base.Required(ErrorMessage) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag SearchAble(bool? cansearch = true)
        {
            return base.SearchAble(cansearch) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag SetColumnWidth(int width)
        {
            return base.SetColumnWidth(width) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag SetDisplayName(string Value)
        {
            return base.SetDisplayName(Value) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag ShowForDisplay(bool show)
        {
            return base.ShowForDisplay(show) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag ShowForEdit(bool show)
        {
            return base.ShowForEdit(show) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag SetTemplate(string template)
        {
            return base.SetTemplate(template) as TextBoxHtmlTag;
        }
        public new TextBoxHtmlTag Format(string format)
        {
            return base.Format(format) as TextBoxHtmlTag;
        }
        #endregion
    }
}
