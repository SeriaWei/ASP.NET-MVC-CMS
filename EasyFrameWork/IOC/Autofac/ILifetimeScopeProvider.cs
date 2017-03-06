/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Autofac;

namespace Easy.IOC.Autofac
{
    public interface ILifetimeScopeProvider
    {
        ILifetimeScope LifetimeScope { get; }
        ILifetimeScope BeginLifetimeScope();
        void EndLifetimeScope(); 
    }
}