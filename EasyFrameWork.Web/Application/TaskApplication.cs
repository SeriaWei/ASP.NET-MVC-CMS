/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using Easy.Extend;
using Easy.StartTask;
using Easy.IOC;
using Easy.Web.Filter;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.Application
{
    public abstract class TaskApplication : HttpApplication
    {
        private readonly TaskManager _taskManager = new TaskManager();
        public TaskManager TaskManager
        {
            get { return _taskManager; }
        }
        private List<Assembly> _assemblies;
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies != null) return _assemblies;
                _assemblies = new List<Assembly>();
                BuildManager.GetReferencedAssemblies().Cast<Assembly>().Each(_assemblies.Add);
                return _assemblies.Where(assembly => !assembly.GlobalAssemblyCache);
            }
        }

        private IEnumerable<Type> _currentTypes;
        public IEnumerable<Type> CurrentTypes
        {
            get { return _currentTypes ?? (_currentTypes = Assemblies.ConcreteTypes()); }
        }
        private IEnumerable<Type> _publicTypes;
        public IEnumerable<Type> PublicTypes
        {
            get { return _publicTypes ?? (_publicTypes = Assemblies.PublicTypes()); }
        }
        public abstract IContainerAdapter ContainerAdapter { get;  }
        public abstract void Application_Starting();

        public virtual void Application_Started()
        {
            ServiceLocator.Current.GetAllInstances<IConfigureFilter>().Each(m => m.Configure());
        }
    }
}