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
            routes.Add(new RouteDescriptor
            {
                Url = "admin/widgettemplate/{action}",
                Defaults = new { controller = "WidgetTemplate",action="index",module="widget" },
                Priority = 1,
                Namespaces = new string[] { "Easy.CMS.Widget.Controllers" }
            });
            routes.Add(new RouteDescriptor
            {
                Url = "admin/widget/{action}",
                Defaults = new { controller = "Widget", action = "index", module = "widget" },
                Priority = 1,
                Namespaces = new string[] { "Easy.CMS.Widget.Controllers" }
            });
            return routes;
        }
    }
}