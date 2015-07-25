using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Constant;

namespace Easy.HTML.Tags
{
    public class MutiSelectHtmlTag : DropDownListHtmlTag
    {
        public MutiSelectHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.MutiSelect;
            this.StartStr = "<select";
            this.EndStr = "></select>";
            this.AddProperty("multiple", "multiple");
        }
    }
}
