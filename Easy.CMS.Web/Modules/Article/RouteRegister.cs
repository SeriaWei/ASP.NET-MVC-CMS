using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Mvc;


namespace Easy.CMS.Article
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
                Defaults = new { controller = "Article", action = "home", module = "Article" },
                Namespaces = new string[] { "Easy.CMS.Article.Controllers" },
                Priority = 1
            });
            return routes;
        }
    }
}