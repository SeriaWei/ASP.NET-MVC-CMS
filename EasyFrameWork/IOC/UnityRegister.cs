using System;
using System.Collections.Generic;
using Easy.Extend;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
namespace Easy.IOC
{
    public class UnityRegister
    {
        private readonly Type _adapterServiceType = typeof(IAdapterService);
        private readonly Type _adapterRepositoryType = typeof(IAdapterRepository);
        private readonly Type _entityType = typeof(IEntity);
        private readonly IUnityContainer _container;
        public UnityRegister(IUnityContainer container)
        {
            _container = container;
            AppDomain.CurrentDomain.GetAssemblies().Each(m => m.GetTypes().Each(p =>
            {
                if (p.IsClass && !p.IsAbstract && !p.IsInterface && !p.IsGenericType)
                {
                    if (_adapterServiceType.IsAssignableFrom(p) ||
                        _adapterRepositoryType.IsAssignableFrom(p) ||
                        _entityType.IsAssignableFrom(p))
                    {
                        if (_entityType.IsAssignableFrom(p))
                        {
                            _container.RegisterType(p);
                        }
                        else
                        {
                            foreach (var inter in p.GetInterfaces())
                            {
                                _container.RegisterType(inter, p, p.FullName);
                            }
                        }
                    }

                }
            }));
        }

        public void Regist()
        {
            var locator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}