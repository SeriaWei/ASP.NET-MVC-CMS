using Easy.Web.Resource.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;

namespace Easy.Web.Resource
{
    public class ResourceEntity
    {
        const string StyleFormt = "<link href=\"{0}\" rel=\"stylesheet\" />";
        const string ScriptFormt = "<script src=\"{0}\" type=\"text/javascript\"></script>";
        public ResourcePosition Position { get; set; }
        public TextWriter Source { get; set; }

        public ResourceType SourceType { get; set; }
        public string ReleaseSource { get; set; }
        public string DebugSource { get; set; }

        public TextWriter ToSource<T>(Page.ViewPage<T> page,HttpContextBase httpContext)
        {
            if (Source != null)
            {
                return Source;
            }
            Page.HtmlStringWriter writer = new Page.HtmlStringWriter();
            if (System.Diagnostics.Debugger.IsAttached || httpContext.IsDebuggingEnabled)
            {
                switch (SourceType)
                {
                    case ResourceType.Script: writer.Write(string.Format(ScriptFormt, page.Url.Content(DebugSource))); break;
                    case ResourceType.Style: writer.Write(string.Format(StyleFormt, page.Url.Content(DebugSource))); break;
                }
            }
            else
            {
                switch (SourceType)
                {
                    case ResourceType.Script: writer.Write(string.Format(ScriptFormt, page.Url.Content(ReleaseSource))); break;
                    case ResourceType.Style: writer.Write(string.Format(StyleFormt, page.Url.Content(ReleaseSource))); break;
                }
            }
            return writer;
        }

        public ResourceEntity ToNew()
        {
            return new ResourceEntity()
            {
                Position = Position,
                Source = Source,
                SourceType = SourceType,
                ReleaseSource = ReleaseSource,
                DebugSource = DebugSource
            };
        }
    }
}
