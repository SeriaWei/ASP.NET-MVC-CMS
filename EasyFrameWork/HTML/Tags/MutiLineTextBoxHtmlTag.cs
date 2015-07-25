using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public class MutiLineTextBoxHtmlTag : HtmlTagBase
    {
        public MutiLineTextBoxHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.MutiLineTextBox;
            this.StartStr = "<textarea";
            this.EndStr = "></textarea>";
            this.AddProperty("cols", "20");
            this.AddProperty("rows", "3");
        }
        public override string ToString()
        {
            string result= base.ToString();
            return result.Replace("</textarea>", this.Value + "</textarea>");
        }
        public override string ToString(bool widthLabel)
        {
            string result = base.ToString(widthLabel);
            return result.Replace("</textarea>", this.Value + "</textarea>");
        }
    }
}
