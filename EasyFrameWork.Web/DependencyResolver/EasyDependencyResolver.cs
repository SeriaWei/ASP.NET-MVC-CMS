using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.DependencyResolver
{
    public class EasyDependencyResolver : IDependencyResolver
    {
        public object GetService(Type serviceType)
        {
            try
            {
                return ServiceLocator.Current.GetInstance(serviceType);
            }
            catch
            {

                return null;
            }
            
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return ServiceLocator.Current.GetAllInstances(serviceType);
            }
            catch
            {
                return null;
            }
            
        }
    }
}
