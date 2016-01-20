using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Standard
{
    public class StatisticsPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            yield return new RouteDescriptor()
            {
                RouteName = "StatisticsPlug",
                Url = "admin/Statistics/{action}",
                Defaults = new { controller = "Statistics", action = "index", module = "Statistics" },
                Namespaces = new string[] { "Easy.CMS.Statistics.Controllers" },
                Priority = 1
            };
            yield return new RouteDescriptor()
            {
                RouteName = "StatisticsPlug_Open",
                Url = "openstatistics/{action}",
                Defaults = new { controller = "Open", action = "index", module = "Statistics" },
                Namespaces = new string[] { "Easy.CMS.Statistics.Controllers" },
                Priority = 1
            };
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            yield return new AdminMenu
            {
                Title = "统计",
                Url = "~/admin/Statistics",
                Icon = "glyphicon-align-justify"
            };
        }

        public override void InitScript()
        {
            
        }

        public override void InitStyle()
        {
           
        }
    }
}