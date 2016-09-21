using System.Collections.Generic;
using Easy.IOC;
using Easy.Web.Resource;
using Easy.Web.Route;

namespace Easy.Web.CMS
{
    public abstract class PluginBase : ResourceManager, IRouteRegister, IModule
    {
        public abstract IEnumerable<RouteDescriptor> RegistRoute();
        public abstract IEnumerable<AdminMenu> AdminMenu();
        public abstract IEnumerable<PermissionDescriptor> RegistPermission();

        public virtual void Load(IContainerAdapter adapter)
        {
            adapter.RegisterType(typeof (PluginBase), GetType());
        }
    }
}
