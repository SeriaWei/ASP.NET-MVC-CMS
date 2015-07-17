using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Product
{
    public class ProductPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            yield return new RouteDescriptor()
            {
                RouteName = "sectionAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new { action = "index", module = "Section" },
                Namespaces = new string[] { "Easy.CMS.Section.Controllers" },
                Priority = 1
            };
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