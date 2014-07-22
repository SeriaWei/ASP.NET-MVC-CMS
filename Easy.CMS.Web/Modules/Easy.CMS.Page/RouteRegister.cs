using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;


namespace PlugWeb.Page
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
                Namespaces = new string[] { "PlugWeb.Page.Controllers" },
                Priority = -1,
                Constraints = new RouteConstraint()
            });
            routes.Add(new RouteDescriptor()
            {
                RouteName = "Design",
                Url = "Design/{action}",
                Defaults = new { controller = "Design", action = "layout" },
                Namespaces = new string[] { "PlugWeb.Page.Controllers" },
                Priority = 1
            });
            routes.Add(new RouteDescriptor()
            {
                RouteName = "pageAdmin",
                Url = "page/admin/{action}",
                Defaults = new { controller = "admin", action = "index" },
                Namespaces = new string[] { "PlugWeb.Page.Controllers" },
                Priority = 1
            });
            return routes;
        }
    }
}