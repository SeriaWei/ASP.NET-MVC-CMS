using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Easy.Web.Route
{
    public interface IRouteRegister
    {
        IEnumerable<RouteDescriptor> RegistRoute();
    }
}
