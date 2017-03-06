/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Web;
using Autofac;
using Easy.IOC.Autofac;

namespace Easy.Web.Application
{
    public class RequestLifetimeScopeProvider : ILifetimeScopeProvider
    {
        private readonly IContainer _container;
        private readonly Type _requestLifetimeScopeProviderKey = typeof(RequestLifetimeScopeProvider);

        public RequestLifetimeScopeProvider(IContainer container)
        {
            this._container = container;
        }

        public ILifetimeScope LifetimeScope
        {
            get { return HttpContext.Current.Items[_requestLifetimeScopeProviderKey] as ILifetimeScope; }
            private set { HttpContext.Current.Items[_requestLifetimeScopeProviderKey] = value; }
        }
       
        public ILifetimeScope BeginLifetimeScope()
        {
            return LifetimeScope ?? (LifetimeScope = _container.BeginLifetimeScope());
        }

        public void EndLifetimeScope()
        {
            if (LifetimeScope != null)
            {
                LifetimeScope.Dispose();
                LifetimeScope = null;
            }
        }
       
    }
}