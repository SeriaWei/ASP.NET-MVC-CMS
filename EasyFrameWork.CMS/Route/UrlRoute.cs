using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using Easy.Extend;
using System.Web.Mvc;

namespace Easy.Web.CMS.Route
{
    class UrlRoute : RouteBase
    {
        public override RouteData GetRouteData(System.Web.HttpContextBase httpContext)
        {
            RouteData data = new RouteData(this, new MvcRouteHandler());
            return data;
        }

        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values)
        {
            return null;
        }
    }
}
