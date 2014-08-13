using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;


namespace Easy.CMS.News
{
    public class RouteRegister : IRouteRegister
    {
        public IEnumerable<RouteDescriptor> Regist()
        {
            List<RouteDescriptor> routes = new List<RouteDescriptor>();            

            routes.Add(new RouteDescriptor()
            {
                RouteName = "newsAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "news", action = "home", module = "news" },
                Namespaces = new string[] { "Easy.CMS.News.Controllers" },
                Priority = 1
            });
            return routes;
        }
    }
}