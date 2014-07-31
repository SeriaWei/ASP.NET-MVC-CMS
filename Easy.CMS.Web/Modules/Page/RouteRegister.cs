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
                Namespaces = new string[] { "Easy.CMS.Page.Controllers" },
                Priority = -1,
                Constraints = new RouteConstraint()
            });
            routes.Add(new RouteDescriptor()
            {
                RouteName = "Design",
                Url = "Design/{action}",
                Defaults = new { controller = "Design", action = "layout" },
                Namespaces = new string[] { "Easy.CMS.Page.Controllers" },
                Priority = 1
            });
            routes.Add(new RouteDescriptor()
            {
                RouteName = "pageAdmin",
                Url = "admin/page/{action}",
                Defaults = new { controller = "admin", action = "index", module = "page" },
                Namespaces = new string[] { "Easy.CMS.Page.Controllers" },
                Priority = 1
            });
            return routes;
        }
    }
}