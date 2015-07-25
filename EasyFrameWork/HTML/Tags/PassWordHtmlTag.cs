using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public class PassWordHtmlTag : HtmlTagBase
    {
        public PassWordHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.PassWord;
            this.StartStr = "<input";
            this.EndStr = "/>";
            this.AddProperty("type", "password");
        }
    }
}
