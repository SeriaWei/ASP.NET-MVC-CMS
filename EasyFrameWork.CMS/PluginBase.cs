using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Web.Resource;
using Easy.Web.Route;

namespace Easy.Web.CMS
{
    public abstract class PluginBase : ResourceManager, IRouteRegister
    {
        public abstract IEnumerable<RouteDescriptor> RegistRoute();
        public abstract IEnumerable<AdminMenu> AdminMenu();
    }
}
