/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.Web.CMS;
using Easy.Web.Resource;
using Easy.Web.Route;

namespace Easy.CMS.Section
{
    public class ProductPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            yield return new RouteDescriptor
            {
                RouteName = "video-play",
                Url = "VideoPlayer/Play",
                Defaults = new { controller = "SectionContentVideo", action = "Play", module = "Section" },
                Namespaces = new[] { "Easy.CMS.Section.Controllers" },
                Priority = 10
            };
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            return null;
        }

        protected override void InitScript(Func<string, ResourceHelper> script)
        {
            
        }

        protected override void InitStyle(Func<string, ResourceHelper> style)
        {
            style("SectionAdmin").Include("~/Modules/Section/Content/Section.css", "~/Modules/Section/Content/Section.min.css");
            style("Section").Include("~/Modules/Section/Content/SectionClient.css", "~/Modules/Section/Content/SectionClient.min.css");
        }

        public override IEnumerable<PermissionDescriptor> RegistPermission()
        {
            return null;
        }
    }
}