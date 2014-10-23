using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Product
{
    public class ProductPlug:PluginBase
    {
        public override IEnumerable<RouteDescriptor> Regist()
        {
            var routes = new List<RouteDescriptor>();
            routes.Add(new RouteDescriptor()
            {
                RouteName = "productAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "product", action = "home", module = "product" },
                Namespaces = new string[] { "Easy.CMS.Product.Controllers" },
                Priority = 1
            });
            return routes;
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            throw new NotImplementedException();
        }

        public override void InitScript()
        {
            
        }

        public override void InitStyle()
        {
            
        }
    }
}