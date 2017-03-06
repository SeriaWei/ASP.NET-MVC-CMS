/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Runtime.InteropServices;
using Autofac;
using Autofac.Builder;

namespace Easy.IOC.Autofac
{
    public class AutofacContainerAdapter : IContainerAdapter
    {
        private readonly ContainerBuilder _builder;
        public AutofacContainerAdapter(ContainerBuilder builder)
        {
            _builder = builder;
        }
        public IContainerAdapter RegisterType(Type type)
        {
            return RegisterType(type, DependencyLifeTime.PerDependency);
        }
        public IContainerAdapter RegisterType(Type type, DependencyLifeTime lifeTime)
        {
            MakeLifeTime(_builder.RegisterType(type), lifeTime);
            return this;
        }

        public IContainerAdapter RegisterType(Type itype, Type type)
        {
            return RegisterType(itype, type, DependencyLifeTime.PerDependency);

        }

        public IContainerAdapter RegisterType(Type itype, Type type, DependencyLifeTime lifeTime)
        {
            MakeLifeTime(_builder.RegisterType(type).As(itype), lifeTime);
            return this;
        }
        public IContainerAdapter RegisterType<T>()
        {
            return RegisterType<T>(DependencyLifeTime.PerDependency);
        }
        public IContainerAdapter RegisterType<T>(DependencyLifeTime lifeTime)
        {
            MakeLifeTime(_builder.RegisterType<T>(), lifeTime);
            return this;
        }
        public IContainerAdapter RegisterType<TIt, T>() where T : TIt
        {
            return RegisterType<TIt, T>(DependencyLifeTime.PerDependency);
        }

        public IContainerAdapter RegisterType<TIt, T>(DependencyLifeTime lifeTime) where T : TIt
        {
            MakeLifeTime(_builder.RegisterType<T>().As<TIt>(), lifeTime);
            return this;
        }

        private void MakeLifeTime<TLimit, TActivatorData, TRegistrationStyle>(
            IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> reg, DependencyLifeTime lifeTime)
        {
            switch (lifeTime)
            {
                case DependencyLifeTime.PerDependency:
                    {
                        reg.InstancePerDependency();
                        break;
                    }
                case DependencyLifeTime.PerRequest:
                    {
                        reg.InstancePerLifetimeScope();
                        break;
                    }
                case DependencyLifeTime.SingleInstance:
                    {
                        reg.SingleInstance();
                        break;
                    }
            }
        }
    }
}