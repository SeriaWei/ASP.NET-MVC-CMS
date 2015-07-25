using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Tags
{
    public class FileHtmlTag : HtmlTagBase
    {
        public FileHtmlTag(Type modelType, string property)
            : base(modelType, property)
        {
            this.TagType = HTMLEnumerate.HTMLTagTypes.File;
            this.StartStr = "<input";
            this.EndStr = "/>";
            this.AddProperty("type", "file");
        }
    }
}
