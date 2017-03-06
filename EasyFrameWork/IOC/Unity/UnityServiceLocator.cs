/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace Easy.IOC.Unity
{
    public class UnityServiceLocator : ServiceLocatorImplBase
    {
        private readonly IUnityContainer _container;
        public UnityServiceLocator(IUnityContainer container)
        {
            _container = container;
        }
        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch
            {
                return new List<object>();
            }
        }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch
            {
                return null;
            }
        }
    }
}