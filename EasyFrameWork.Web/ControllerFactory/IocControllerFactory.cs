using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.Web.ControllerFactory
{
    class IocControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            return base.GetControllerInstance(requestContext, controllerType);
        }
    }
}
