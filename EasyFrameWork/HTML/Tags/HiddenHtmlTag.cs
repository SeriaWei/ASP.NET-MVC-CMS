using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public class HiddenHtmlTag : HtmlTagBase
    {
        public HiddenHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.Hidden;
            this.StartStr = "<input";
            this.EndStr = "/>";
            this.AddProperty("type", "hidden");
        }
    }
}
