/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.ViewPort.Validator;

namespace Easy.ViewPort.Descriptor
{
    public class CheckBoxDescriptor : BaseDescriptor
    {
        public CheckBoxDescriptor(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.CheckBox;
            this.TemplateName = "CheckBox";
        }

        #region Regular

        public CheckBoxDescriptor Required(string errorMessage)
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name,
                ErrorMessage = errorMessage
            });
            this.IsRequired = true;
            return this;
        }
        public CheckBoxDescriptor Required()
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name
            });
            this.IsRequired = true;
            return this;
        }

        public CheckBoxDescriptor SetDisplayName(string name)
        {
            this.DisplayName = name;
            foreach (ValidatorBase item in this.Validator)
            {
                item.DisplayName = name;
            }
            return this;
        }
        public CheckBoxDescriptor AddProperty(string property, string value)
        {
            if (this.Properties.ContainsKey(property))
                this.Properties[property] = value;
            else this.Properties.Add(property, value);
            return this;
        }
        public CheckBoxDescriptor AddClass(string name)
        {
            if (!this.Classes.Contains(name))
                this.Classes.Add(name);
            return this;
        }
        public CheckBoxDescriptor SetColumnWidth(int width)
        {
            this.GridSetting.ColumnWidth = width;
            return this;
        }
        public CheckBoxDescriptor SearchAble(bool? cansearch = true)
        {
            bool search = cansearch ?? true;
            this.GridSetting.Searchable = search;
            return this;
        }
        public CheckBoxDescriptor ReadOnly()
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

        public CheckBoxDescriptor AddStyle(string properyt, string value)
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
        public CheckBoxDescriptor Hide()
        {
            this.IsHidden = true;
            return this;
        }
        public CheckBoxDescriptor Ignore()
        {
            this.IsIgnore = true;
            return this;
        }
        public CheckBoxDescriptor HideInGrid()
        {
            this.GridSetting.Visiable = false;
            return this;
        }
        public CheckBoxDescriptor Order(int index)
        {
            this.OrderIndex = index;
            return this;
        }
        public CheckBoxDescriptor ShowForDisplay(bool show)
        {
            this.IsShowForDisplay = show;
            return this;
        }
        public CheckBoxDescriptor ShowForEdit(bool show)
        {
            this.IsShowForEdit = show;
            return this;
        }
        public CheckBoxDescriptor SetTemplate(string template)
        {
            this.TemplateName = template;
            return this;
        }

        #endregion
    }
}
