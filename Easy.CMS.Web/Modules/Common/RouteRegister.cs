using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;


namespace Easy.CMS.Layout
{
    public class RouteRegister : IRouteRegister
    {
        public IEnumerable<RouteDescriptor> Regist()
        {
            List<RouteDescriptor> routes = new List<RouteDescriptor>();

            routes.Add(new RouteDescriptor()
            {
                RouteName = "pageRoute",
                Url = "{*path}",
                Defaults = new { controller = "Page", action = "PreView", path = UrlParameter.Optional },
                Namespaces = new string[] { "Easy.CMS.Common.Controllers" },
                Priority = -1,
                Constraints = new RouteConstraint()
            });

            routes.Add(new RouteDescriptor()
            {
                RouteName = "layoutAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "layout", action = "index", module = "common" },
                Namespaces = new string[] { "Easy.CMS.Common.Controllers" },
                Priority = 1
            });
            return routes;
        }
    }
}