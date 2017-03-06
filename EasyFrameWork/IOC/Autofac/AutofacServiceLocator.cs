/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using Microsoft.Practices.ServiceLocation;

namespace Easy.IOC.Autofac
{
    public class AutofacServiceLocator : ServiceLocatorImplBase
    {
        readonly IContainer _container;

        public AutofacServiceLocator(IContainer container)
        {
            if (container == null)
                throw new ArgumentNullException("container");
            _container = container;
            if (container.IsRegistered<ILifetimeScopeProvider>())
            {
                LifetimeScopeProvider =
                    container.Resolve<ILifetimeScopeProvider>(new NamedParameter("container", container));
            }

        }
        public ILifetimeScopeProvider LifetimeScopeProvider { get; private set; }

        protected override object DoGetInstance(Type serviceType, string key)
        {
            try
            {
                if (_container.IsRegistered(serviceType))
                {
                    if (LifetimeScopeProvider != null && LifetimeScopeProvider.LifetimeScope != null)
                    {
                        var scope = LifetimeScopeProvider.LifetimeScope;
                        return key != null ? scope.ResolveNamed(key, serviceType) : scope.Resolve(serviceType);
                    }
                    else
                    {
                        return key != null ? _container.ResolveNamed(key, serviceType) : _container.Resolve(serviceType);
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType)
        {
            try
            {
                if (_container.IsRegistered(serviceType))
                {
                    if (LifetimeScopeProvider != null && LifetimeScopeProvider.LifetimeScope != null)
                    {
                        var scope = LifetimeScopeProvider.LifetimeScope;
                        var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                        object instance = scope.Resolve(enumerableType);
                        return ((IEnumerable)instance).Cast<object>();
                    }
                    else
                    {
                        var enumerableType = typeof(IEnumerable<>).MakeGenericType(serviceType);
                        object instance = _container.Resolve(enumerableType);
                        return ((IEnumerable)instance).Cast<object>();
                    }
                }
                return new List<object>();
            }
            catch
            {
                return new List<object>();
            }
        }
    }
}