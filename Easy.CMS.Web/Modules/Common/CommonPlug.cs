using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Common
{
    public class CommonPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> Regist()
        {
            var routes = new List<RouteDescriptor>();

            routes.Add(new RouteDescriptor
            {
                RouteName = "pageRoute",
                Url = "{*path}",
                Defaults = new { controller = "Page", action = "PreView", path = UrlParameter.Optional },
                Namespaces = new string[] { "Easy.Web.CMS.Common.Controllers" },
                Priority = -1,
               // Constraints = new { path = new RouteConstraint() }
            });

            routes.Add(new RouteDescriptor
            {
                RouteName = "layoutAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "layout", action = "index", module = "common" },
                Namespaces = new string[] { "Easy.Web.CMS.Common.Controllers" },
                Priority = 10
            });

            return routes;
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            throw new NotImplementedException();
        }

        public override void InitScript()
        {
            Script("Navigation").Include("~/Modules/Common/Scripts/Navigation.js");
        }

        public override void InitStyle()
        {
            Style("Layout").Include("~/Modules/Common/Content/Layout.css");
        }
    }
}