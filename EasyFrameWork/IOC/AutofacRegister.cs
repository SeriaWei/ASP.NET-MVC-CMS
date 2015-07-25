using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Easy.IOC.Autofac;
using Microsoft.Practices.ServiceLocation;
using Easy.Extend;
using Easy.Models;

namespace Easy.IOC
{
    public class AutofacRegister
    {
        readonly Type _adapterServiceType = typeof(IAdapterService);
        readonly Type _adapterRepositoryType = typeof(IAdapterRepository);
        readonly Type _entityType = typeof(IEntity);

        public AutofacRegister(ContainerBuilder builder)
        {
            List<Type> adapterServiceTypes = new List<Type>();
            AppDomain.CurrentDomain.GetAssemblies().Each(m => m.GetTypes().Each(p =>
            {
                if (p.IsClass && !p.IsAbstract && !p.IsInterface && !p.IsGenericType)
                {
                    if (_adapterServiceType.IsAssignableFrom(p) ||
                        _adapterRepositoryType.IsAssignableFrom(p) ||
                        _entityType.IsAssignableFrom(p))
                    {
                        adapterServiceTypes.Add(p);
                    }

                }
            }));
            foreach (var type in adapterServiceTypes)
            {
                if (_entityType.IsAssignableFrom(type))
                {
                    builder.RegisterType(type);
                }
                else
                {
                    builder.RegisterType(type).As(type.GetInterfaces());
                }

            }
        }
        public void Regist(IContainer container)
        {
            var locator = new AutofacServiceLocator(container);

            ServiceLocator.SetLocatorProvider(() => locator);
        }
    }
}