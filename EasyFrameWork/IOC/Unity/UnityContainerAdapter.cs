/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using Microsoft.Practices.Unity;

namespace Easy.IOC.Unity
{
    public class UnityContainerAdapter : IContainerAdapter
    {
        private readonly IUnityContainer _container;

        public UnityContainerAdapter(IUnityContainer container)
        {
            _container = container;
        }
        public IContainerAdapter RegisterType(Type type)
        {
            return RegisterType(type, DependencyLifeTime.PerDependency);
        }
        public IContainerAdapter RegisterType(Type type, DependencyLifeTime lifeTime)
        {
            _container.RegisterType(type, GetLifetimeManager(lifeTime));
            _container.RegisterType(type, type.FullName, GetLifetimeManager(lifeTime));
            return this;
        }

        public IContainerAdapter RegisterType(Type itype, Type type)
        {
            return RegisterType(itype, type, DependencyLifeTime.PerDependency);
        }


        public IContainerAdapter RegisterType(Type itype, Type type, DependencyLifeTime lifeTime)
        {
            _container.RegisterType(itype, type, GetLifetimeManager(lifeTime));
            _container.RegisterType(itype, type, itype.Name + type.FullName, GetLifetimeManager(lifeTime));
            return this;
        }
        public IContainerAdapter RegisterType<T>()
        {
            return RegisterType<T>(DependencyLifeTime.PerDependency);
        }
        public IContainerAdapter RegisterType<T>(DependencyLifeTime lifeTime)
        {
            _container.RegisterType<T>(GetLifetimeManager(lifeTime));
            _container.RegisterType<T>(typeof(T).FullName, GetLifetimeManager(lifeTime));
            return this;
        }
        public IContainerAdapter RegisterType<TIt, T>() where T : TIt
        {
            return RegisterType<TIt, T>(DependencyLifeTime.PerDependency);
        }

        public IContainerAdapter RegisterType<TIt, T>(DependencyLifeTime lifeTime) where T : TIt
        {
            _container.RegisterType<TIt, T>(GetLifetimeManager(lifeTime));
            _container.RegisterType<TIt, T>(typeof(TIt).Name + typeof(T).FullName, GetLifetimeManager(lifeTime));
            return this;
        }

        LifetimeManager GetLifetimeManager(DependencyLifeTime lifeTime)
        {
            LifetimeManager lifetimeManager = null;
            switch (lifeTime)
            {
                case DependencyLifeTime.PerDependency:
                    {
                        lifetimeManager = new PerResolveLifetimeManager();
                        break;
                    }
                case DependencyLifeTime.PerRequest:
                    {
                        lifetimeManager = new PerRequestLifetimeManager();
                        break;
                    }
                case DependencyLifeTime.SingleInstance:
                    {
                        lifetimeManager = new ContainerControlledLifetimeManager();
                        break;
                    }
                default:
                    {
                        lifetimeManager = new PerResolveLifetimeManager();
                        break;
                    }
            }
            return lifetimeManager;
        }
    }
}