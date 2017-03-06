/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.ViewPort.Validator;

namespace Easy.ViewPort.Descriptor
{
    public class HiddenDescriptor : BaseDescriptor
    {
        public HiddenDescriptor(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.Hidden;
            this.TemplateName = "Hidden";
        }
        #region Regular

        public HiddenDescriptor Required(string errorMessage)
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name,
                ErrorMessage = errorMessage
            });
            this.IsRequired = true;
            return this;
        }
        public HiddenDescriptor Required()
        {
            this.Validator.Add(new RequiredValidator()
            {
                Property = this.Name
            });
            this.IsRequired = true;
            return this;
        }

        public HiddenDescriptor SetDisplayName(string name)
        {
            this.DisplayName = name;
            foreach (ValidatorBase item in this.Validator)
            {
                item.DisplayName = name;
            }
            return this;
        }
        public HiddenDescriptor AddProperty(string property, string value)
        {
            if (this.Properties.ContainsKey(property))
                this.Properties[property] = value;
            else this.Properties.Add(property, value);
            return this;
        }
        public HiddenDescriptor AddClass(string name)
        {
            if (!this.Classes.Contains(name))
                this.Classes.Add(name);
            return this;
        }
        public HiddenDescriptor SetColumnWidth(int width)
        {
            this.GridSetting.ColumnWidth = width;
            return this;
        }
        public HiddenDescriptor SearchAble(bool? cansearch = true)
        {
            bool search = cansearch ?? true;
            this.GridSetting.Searchable = search;
            return this;
        }
        public HiddenDescriptor ReadOnly()
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

        public HiddenDescriptor AddStyle(string properyt, string value)
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
        public HiddenDescriptor Hide()
        {
            this.IsHidden = true;
            return this;
        }
        public HiddenDescriptor Ignore()
        {
            this.IsIgnore = true;
            return this;
        }
        public HiddenDescriptor HideInGrid()
        {
            this.GridSetting.Visiable = false;
            return this;
        }
        public HiddenDescriptor Order(int index)
        {
            this.OrderIndex = index;
            return this;
        }
        public HiddenDescriptor ShowForDisplay(bool show)
        {
            this.IsShowForDisplay = show;
            return this;
        }
        public HiddenDescriptor ShowForEdit(bool show)
        {
            this.IsShowForEdit = show;
            return this;
        }
        public HiddenDescriptor SetTemplate(string template)
        {
            this.TemplateName = template;
            return this;
        }

        #endregion
    }
}
