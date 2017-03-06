/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.ViewPort.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.ViewPort.Descriptor
{
    public class TextAreaDescriptor : BaseDescriptor
    {
        public TextAreaDescriptor(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.MutiLineTextBox;
            this.TemplateName = "TextArea";
        }
        #region Regular

        public TextAreaDescriptor Required(string errorMessage)
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name,
                ErrorMessage = errorMessage
            });
            this.IsRequired = true;
            return this;
        }
        public TextAreaDescriptor Required()
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name
            });
            this.IsRequired = true;
            return this;
        }

        public TextAreaDescriptor SetDisplayName(string name)
        {
            this.DisplayName = name;
            foreach (ValidatorBase item in this.Validator)
            {
                item.DisplayName = name;
            }
            return this;
        }
        public TextAreaDescriptor AddProperty(string property, string value)
        {
            if (this.Properties.ContainsKey(property))
                this.Properties[property] = value;
            else this.Properties.Add(property, value);
            return this;
        }
        public TextAreaDescriptor AddClass(string name)
        {
            if (!this.Classes.Contains(name))
                this.Classes.Add(name);
            return this;
        }
        public TextAreaDescriptor SetColumnWidth(int width)
        {
            this.GridSetting.ColumnWidth = width;
            return this;
        }
        public TextAreaDescriptor SearchAble(bool? cansearch = true)
        {
            bool search = cansearch ?? true;
            this.GridSetting.Searchable = search;
            return this;
        }
        public TextAreaDescriptor ReadOnly()
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

        public TextAreaDescriptor AddStyle(string properyt, string value)
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
        public TextAreaDescriptor Hide()
        {
            this.IsHidden = true;
            return this;
        }
        public TextAreaDescriptor Ignore()
        {
            this.IsIgnore = true;
            return this;
        }
        public TextAreaDescriptor HideInGrid()
        {
            this.GridSetting.Visiable = false;
            return this;
        }
        public TextAreaDescriptor Order(int index)
        {
            this.OrderIndex = index;
            return this;
        }
        public TextAreaDescriptor ShowForDisplay(bool show)
        {
            this.IsShowForDisplay = show;
            return this;
        }
        public TextAreaDescriptor ShowForEdit(bool show)
        {
            this.IsShowForEdit = show;
            return this;
        }
        public TextAreaDescriptor SetTemplate(string template)
        {
            this.TemplateName = template;
            return this;
        }

        #endregion

        public TextAreaDescriptor MaxLength(int max)
        {
            this.Validator.Add(new StringLengthValidator(0, max)
            {
                Property = this.Name
            });
            return this;
        }
        public TextAreaDescriptor MaxLength(int max, string errorMsg)
        {
            this.Validator.Add(new StringLengthValidator(0, max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
        public TextAreaDescriptor MaxLength(int min, int max)
        {
            this.Validator.Add(new StringLengthValidator(min, max)
            {
                Property = this.Name
            });
            return this;
        }
        public TextAreaDescriptor MaxLength(int min, int max, string errorMsg)
        {
            this.Validator.Add(new StringLengthValidator(min, max)
            {
                ErrorMessage = errorMsg,
                Property = this.Name
            });
            return this;
        }
    }
}
