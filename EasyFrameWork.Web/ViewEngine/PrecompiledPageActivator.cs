using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace Easy.Web.ViewEngine
{
    public class PrecompiledPageActivator : IViewPageActivator
    {
        public object Create(ControllerContext controllerContext, Type type)
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService(type);
        }
    }
}
