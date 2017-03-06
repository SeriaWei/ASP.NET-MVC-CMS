/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using Easy.Reflection;

namespace Easy.IOC.Unity
{
    public sealed class UnityRegister : AssemblyInfo
    {
        private readonly IUnityContainer _container;
        public UnityRegister(IUnityContainer container)
        {
            _container = container;
            PublicTypes.Each(p =>
            {
                if (p != null && p.IsClass && !p.IsAbstract && !p.IsInterface && !p.IsGenericType)
                {
                    if ((KnownTypes.DependencyType.IsAssignableFrom(p) ||
                        KnownTypes.EntityType.IsAssignableFrom(p)) && !KnownTypes.FreeDependencyType.IsAssignableFrom(p))
                    {
                        if (KnownTypes.EntityType.IsAssignableFrom(p))
                        {
                            _container.RegisterType(p, GetLifetimeManager(p));
                        }
                        else
                        {
                            foreach (var inter in p.GetInterfaces())
                            {
                                _container.RegisterType(inter, p, GetLifetimeManager(p));
                                _container.RegisterType(inter, p, inter.Name + p.FullName, GetLifetimeManager(p));
                            }
                            if (p.BaseType != null && p.BaseType.IsAbstract)
                            {
                                RegistBaseType(p, p.BaseType);
                            }
                        }
                    }
                    if (KnownTypes.ModuleType.IsAssignableFrom(p))
                    {
                        ((IModule)Activator.CreateInstance(p)).Load(new UnityContainerAdapter(container));
                    }

                }
            });
        }

        private void RegistBaseType(Type type, Type baseType)
        {
            if (type != KnownTypes.ObjectType && baseType != null)
            {
                _container.RegisterType(baseType, type, baseType.Name + type.FullName, GetLifetimeManager(type));
                RegistBaseType(type, baseType.BaseType);
            }
        }

        public void Regist()
        {
            var locator = new UnityServiceLocator(_container);
            ServiceLocator.SetLocatorProvider(() => locator);
        }


        LifetimeManager GetLifetimeManager(Type lifeTimeType)
        {
            LifetimeManager lifetimeManager;
            if (KnownTypes.SingleInstanceType.IsAssignableFrom(lifeTimeType))
            {
                lifetimeManager = new ContainerControlledLifetimeManager();
            }
            else if (KnownTypes.PerRequestType.IsAssignableFrom(lifeTimeType))
            {
                lifetimeManager = new PerRequestLifetimeManager();
            }
            else
            {
                lifetimeManager = new PerResolveLifetimeManager();
            }
            return lifetimeManager;
        }
    }
}