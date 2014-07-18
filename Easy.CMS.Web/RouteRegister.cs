using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PlugWeb
{
    public class RouteRegister : Easy.Web.Route.IRouteRegister
    {
        public IEnumerable<Easy.Web.Route.RouteDescriptor> Regist()
        {
            List<RouteDescriptor> routes = new List<RouteDescriptor>();
            //routes.Add(new RouteDescriptor()
            //{
            //    RouteName = "default",
            //    Url = "{controller}/{action}/{id}",
            //    Defaults = new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //});
            return routes;
        }
    }
}