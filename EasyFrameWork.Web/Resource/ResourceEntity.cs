/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Web.Resource.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.WebPages;
using Easy.Extend;

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
        public string CDNSource { get; set; }
        public bool UseCNDSource
        {
            get
            {
                var setting = System.Configuration.ConfigurationManager.AppSettings["UseCDN"];
                return setting.IsNotNullAndWhiteSpace() && setting.Equals("true", StringComparison.CurrentCultureIgnoreCase) && CDNSource.IsNotNullAndWhiteSpace();
            }
        }

        public TextWriter ToSource<T>(Page.ViewPage<T> page, HttpContextBase httpContext)
        {
            if (Source != null)
            {
                return Source;
            }
            Page.HtmlStringWriter writer = new Page.HtmlStringWriter();
            string source = null;
            if (System.Diagnostics.Debugger.IsAttached || httpContext.IsDebuggingEnabled)
            {
                switch (SourceType)
                {
                    case ResourceType.Script: source = string.Format(ScriptFormt, page.Url.Content(DebugSource)); break;
                    case ResourceType.Style: source = string.Format(StyleFormt, page.Url.Content(DebugSource)); break;
                }
            }
            else
            {
                switch (SourceType)
                {
                    case ResourceType.Script: source = string.Format(ScriptFormt, UseCNDSource ? CDNSource : page.Url.Content(ReleaseSource)); break;
                    case ResourceType.Style: source = string.Format(StyleFormt, UseCNDSource ? CDNSource : page.Url.Content(ReleaseSource)); break;
                }
            }
            writer.Write(source);
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
                CDNSource = CDNSource,
                DebugSource = DebugSource
            };
        }
    }
}
