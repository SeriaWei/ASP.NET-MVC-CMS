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
                RouteName = "layoutAdmin",
                Url = "layout/admin/{action}",
                Defaults = new { controller = "admin", action = "index", module = "layout" },
                Namespaces = new string[] { "Easy.CMS.Layout.Controllers" },
                Priority = 1
            });
            return routes;
        }
    }
}