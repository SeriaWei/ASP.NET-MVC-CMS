using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.ControllerActivator
{
    public class EasyControllerActivator : IControllerActivator
    {

        public IController Create(System.Web.Routing.RequestContext requestContext, System.Type controllerType)
        {
            return System.Web.Mvc.DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}