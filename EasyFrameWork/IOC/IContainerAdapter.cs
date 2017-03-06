/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;

namespace Easy.IOC
{
    public interface IContainerAdapter
    {
        IContainerAdapter RegisterType(Type type);
        IContainerAdapter RegisterType(Type type, DependencyLifeTime lifeTime);
        IContainerAdapter RegisterType(Type itype, Type type);
        IContainerAdapter RegisterType(Type itype, Type type, DependencyLifeTime lifeTime);
        IContainerAdapter RegisterType<T>();
        IContainerAdapter RegisterType<T>(DependencyLifeTime lifeTime);
        IContainerAdapter RegisterType<TIt, T>() where T : TIt;
        IContainerAdapter RegisterType<TIt, T>(DependencyLifeTime lifeTime) where T : TIt;
    }
}