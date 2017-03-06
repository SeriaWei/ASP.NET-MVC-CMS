/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Easy.Web.Filter
{
    public class FilterRegisterItem
    {
        private readonly ParameterDescriptor[] _actionParameterDescriptors;
        public FilterRegisterItem(Type controllerType, ReflectedActionDescriptor actionDescriptor, Type[] filterTypes)
        {
            ControllerType = controllerType;
            ActionDescriptor = actionDescriptor;
            _actionParameterDescriptors = ActionDescriptor.GetParameters();
            FilterTypes = filterTypes;
            Filters = () =>
            {
                if (FilterAttributesInstance != null)
                {
                    return FilterAttributesInstance;
                }
                return FilterAttributesInstance = FilterTypes.Select(f => Activator.CreateInstance(f) as FilterAttribute).ToList();
            };
        }
        public Type ControllerType { get; private set; }
        public ReflectedActionDescriptor ActionDescriptor { get; private set; }
        public Type[] FilterTypes { get; private set; }
        public Func<IEnumerable<FilterAttribute>> Filters;
        protected IEnumerable<FilterAttribute> FilterAttributesInstance;
        public bool IsSameAction(ActionDescriptor descriptor)
        {
            var desc = descriptor as ReflectedActionDescriptor;
            if (desc != null)
            {
                bool sameAction = ActionDescriptor.MethodInfo == desc.MethodInfo;
                if (sameAction)
                    return true;
            }
            ParameterDescriptor[] parameters1 = descriptor.GetParameters();

            bool same = descriptor.ControllerDescriptor.ControllerName.Equals(ActionDescriptor.ControllerDescriptor.ControllerName, StringComparison.OrdinalIgnoreCase) &&
                        descriptor.ActionName.Equals(ActionDescriptor.ActionName, StringComparison.OrdinalIgnoreCase) &&
                        (parameters1.Length == _actionParameterDescriptors.Length);

            if (same)
            {
                for (int i = parameters1.Length - 1; i >= 0; i--)
                {
                    if (parameters1[i].ParameterType == _actionParameterDescriptors[i].ParameterType)
                    {
                        continue;
                    }

                    same = false;
                    break;
                }
            }

            return same;
        }
    }

    public class FilterRegisterConfigureItem<T> : FilterRegisterItem where T : FilterAttribute
    {
        public FilterRegisterConfigureItem(Type controllerType, ReflectedActionDescriptor actionDescriptor, Action<T> configureFilter, Type[] filterTypes)
            : base(controllerType, actionDescriptor, filterTypes)
        {
            ConfigureFilter = configureFilter;
            Filters = () =>
            {
                if (FilterAttributesInstance != null)
                {
                    return FilterAttributesInstance;
                }
                return FilterAttributesInstance = FilterTypes.Select(f =>
                {
                    var attribute = Activator.CreateInstance(f) as FilterAttribute;
                    configureFilter.Invoke(attribute as T);
                    return attribute;
                }).ToList();
            };
        }
        public Action<T> ConfigureFilter { get; private set; }
    }
}