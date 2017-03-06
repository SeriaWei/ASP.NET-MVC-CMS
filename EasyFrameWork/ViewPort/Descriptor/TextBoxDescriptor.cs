/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.ViewPort.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.ViewPort.Descriptor
{
    public class TextBoxDescriptor : BaseDescriptor
    {
        public TextBoxDescriptor(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.Input;
        }

        public string DateFormat { get; set; }
        public string JavaScriptDateFormat { get; set; }

        #region Regular

        public TextBoxDescriptor Required(string errorMessage)
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name,
                ErrorMessage = errorMessage
            });
            this.IsRequired = true;
            return this;
        }
        public TextBoxDescriptor Required()
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name
            });
            this.IsRequired = true;
            return this;
        }

        public TextBoxDescriptor SetDisplayName(string name)
        {
            this.DisplayName = name;
            foreach (ValidatorBase item in this.Validator)
            {
                item.DisplayName = name;
            }
            return this;
        }
        public TextBoxDescriptor AddProperty(string property, string value)
        {
            if (this.Properties.ContainsKey(property))
                this.Properties[property] = value;
            else this.Properties.Add(property, value);
            return this;
        }
        public TextBoxDescriptor AddClass(string name)
        {
            if (!this.Classes.Contains(name))
                this.Classes.Add(name);
            return this;
        }
        public TextBoxDescriptor SetColumnWidth(int width)
        {
            this.GridSetting.ColumnWidth = width;
            return this;
        }
        public TextBoxDescriptor SearchAble(bool? cansearch = true)
        {
            bool search = cansearch ?? true;
            this.GridSetting.Searchable = search;
            return this;
        }
        public TextBoxDescriptor ReadOnly()
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

        public TextBoxDescriptor AddStyle(string properyt, string value)
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
        public TextBoxDescriptor Hide()
        {
            this.IsHidden = true;
            return this;
        }
        public TextBoxDescriptor Ignore()
        {
            this.IsIgnore = true;
            return this;
        }
        public TextBoxDescriptor HideInGrid()
        {
            this.GridSetting.Visiable = false;
            return this;
        }
        public TextBoxDescriptor Order(int index)
        {
            this.OrderIndex = index;
            return this;
        }
        public TextBoxDescriptor ShowForDisplay(bool show)
        {
            this.IsShowForDisplay = show;
            return this;
        }
        public TextBoxDescriptor ShowForEdit(bool show)
        {
            this.IsShowForEdit = show;
            return this;
        }
        public TextBoxDescriptor SetTemplate(string template)
        {
            this.TemplateName = template;
            return this;
        }

        #endregion

        public TextBoxDescriptor FormatAsDate()
        {
            FormatDate("yyyy/MM/dd");
            FormatDateForJavaScript("YYYY/MM/DD");
            this.AddClass("Date");
            return this;
        }
        
        public TextBoxDescriptor FormatAsDateTime()
        {
            FormatDate("yyyy/MM/dd hh:mm");
            FormatDateForJavaScript("YYYY/MM/DD HH:mm");
            return this;
        }
        public TextBoxDescriptor FormatDate(string format)
        {
            this.DateFormat = format;
            this.AddProperty("DateFormat", format);
            this.AddProperty("ValueType", "Date");
            this.AddClass("Date");
            return this;
        }
        public TextBoxDescriptor FormatDateForJavaScript(string format)
        {
            this.JavaScriptDateFormat = format;
            this.AddProperty("JsDateFormat", format);
            this.AddProperty("ValueType", "Date");
            this.AddClass("Date");
            return this;
        }
        public TextBoxDescriptor MaxLength(int max)
        {
            this.Validator.Add(new StringLengthValidator(0, max)
            {
                Property = this.Name
            });
            return this;
        }
        public TextBoxDescriptor MaxLength(int max, string errorMsg)
        {
            this.Validator.Add(new StringLengthValidator(0, max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        public TextBoxDescriptor MaxLength(int min, int max)
        {
            this.Validator.Add(new StringLengthValidator(min, max)
            {
                Property = this.Name
            });
            return this;
        }
        public TextBoxDescriptor MaxLength(int min, int max, string errorMsg)
        {
            this.Validator.Add(new StringLengthValidator(min, max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        public TextBoxDescriptor PlaceHolder(string info)
        {
            this.AddProperty("placeholder", info);
            return this;
        }

        public TextBoxDescriptor Email()
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
        public TextBoxDescriptor Email(string errorMsg)
        {
            this.Validator.Add(new RegularValidator(Constant.RegularExpression.Email)
            {
                ErrorMessage = errorMsg,
                Property = this.Name,
                DisplayName = this.DisplayName
            });
            return this.Email();
        }


        public TextBoxDescriptor RegularExpression(string expression)
        {
            this.Validator.Add(new RegularValidator(expression)
            {
                Property = this.Name
            });
            return this;
        }


        public TextBoxDescriptor RegularExpression(string expression, string errorMsg)
        {
            this.Validator.Add(new RegularValidator(expression)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }

        public TextBoxDescriptor Range(double min, double max)
        {
            this.Validator.Add(new RangeValidator(min, max)
            {
                Property = this.Name
            });
            return this;
        }

        public TextBoxDescriptor Range(double min, double max, string errorMsg)
        {
            this.Validator.Add(new RangeValidator(min, max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }

        public TextBoxDescriptor Remote(string action, string controller)
        {
            this.Validator.Add(new RemoteValidator()
            {
                Action = action,
                Controller = controller,
                Property = this.Name
            });
            return this;
        }

        public TextBoxDescriptor Remote(string action, string controller, string errorMsg)
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

        public TextBoxDescriptor Remote(string action, string controller, string area, string errorMsg)
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

        public TextBoxDescriptor Remote(RemoteValidator validator)
        {
            this.Validator.Add(validator);
            return this;
        }

        public TextBoxDescriptor Format(string format)
        {
            this.ValueFormat = format;
            return this;
        }

        
        
    }
}
