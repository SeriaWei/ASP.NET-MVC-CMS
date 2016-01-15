using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Standard
{
    public class StandardPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            yield return new RouteDescriptor()
            {
                RouteName = "StandardPlug",
                Url = "Standard/{controller}/{action}",
                Defaults = new { action = "index", module = "Standard" },
                Namespaces = new string[] { "Easy.CMS.Standard" },
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