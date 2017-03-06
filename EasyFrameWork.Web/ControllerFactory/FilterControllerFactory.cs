/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy.Web.ActionInvoker;

namespace Easy.Web.ControllerFactory
{
    class FilterControllerFactory : DefaultControllerFactory
    {
        protected override IController GetControllerInstance(System.Web.Routing.RequestContext requestContext, Type controllerType)
        {
            if (controllerType != null)
            {
                var controller = base.GetControllerInstance(requestContext, controllerType) as System.Web.Mvc.Controller;
                if (controller != null)
                    controller.ActionInvoker = new FiltersAsyncControllerActionInvoker();
                return controller;
            }
            return null;
        }
    }
}
