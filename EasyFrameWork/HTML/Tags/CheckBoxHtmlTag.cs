using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public class CheckBoxHtmlTag : HtmlTagBase
    {
        public CheckBoxHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.CheckBox;
            this.StartStr = "<input";
            this.EndStr = "/>";
            this.AddProperty("type", "checkbox");
            this.Value = false;
        }

        public override string ToString()
        {
            //return base.ToString();
            return string.Format("{0}{1}", base.ToString(), "<input name='" + this.Name + "' type='hidden' value='false'/>");
        }
        public override string ToString(bool widthLabel)
        {
            //return base.ToString(widthLabel);
            return string.Format("{0}{1}", base.ToString(widthLabel), "<input name='" + this.Name + "' type='hidden' value='false'/>");
        }
    }
}
