using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;

namespace Easy.Web.Route
{
    public class RouteConstraint : IRouteConstraint
    {
        public bool Match(System.Web.HttpContextBase httpContext, System.Web.Routing.Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            return true;
        }
    }
}
