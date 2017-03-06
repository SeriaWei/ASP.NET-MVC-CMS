/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Easy.Extend;
using Easy.IOC;

namespace Easy.Reflection
{
    public abstract class AssemblyInfo
    {
        public class KnownTypes
        {
            public static readonly Type DependencyType = typeof(IDependency);
            public static readonly Type FreeDependencyType = typeof(IFreeDependency);
            public static readonly Type EntityType = typeof(IEntity);
            public static readonly Type ModuleType = typeof(IModule);
            public static readonly Type SingleInstanceType = typeof (ISingleInstance);
            public static readonly Type PerRequestType = typeof (IPerRequestInstance);
            public static readonly Type ObjectType = typeof (object);
        }



        private List<Assembly> _assemblies;
        public virtual IEnumerable<Assembly> Assemblies
        {
            get
            {
                if (_assemblies != null) return _assemblies;
                _assemblies = new List<Assembly>();
                AppDomain.CurrentDomain.GetAssemblies()
                    .Each(_assemblies.Add);
                return _assemblies.Where(assembly => !assembly.GlobalAssemblyCache);
            }
        }

        private IEnumerable<Type> _currentTypes;
        public virtual IEnumerable<Type> CurrentTypes
        {
            get { return _currentTypes ?? (_currentTypes = Assemblies.ConcreteTypes()); }
        }
        private IEnumerable<Type> _publicTypes;
        public virtual IEnumerable<Type> PublicTypes
        {
            get { return _publicTypes ?? (_publicTypes = Assemblies.PublicTypes()); }
        }
    }
}