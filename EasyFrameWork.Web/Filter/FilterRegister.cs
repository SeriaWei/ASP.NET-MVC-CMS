/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using Easy.Extend;
using Easy.Security;

namespace Easy.Web.Filter
{
    public class FilterRegister : IFilterRegister
    {
        private readonly Dictionary<Type, List<FilterRegisterItem>> _filterRegisterItems;

        public FilterRegister()
        {
            _filterRegisterItems = new Dictionary<Type, List<FilterRegisterItem>>();
        }

        public void Register<TController, TFilterAttribute>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute));
        }
        public void Register<TController, TFilterAttribute>(Expression<Action<TController>> action, Action<TFilterAttribute> configFilter)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute : FilterAttribute
        {
            Register(action, configFilter, typeof(TFilterAttribute));
        }
        public void Register<TController, TFilterAttribute1, TFilterAttribute2>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2));
        }

        public void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2), typeof(TFilterAttribute3));
        }
        public void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2), typeof(TFilterAttribute3), typeof(TFilterAttribute4));
        }

        public void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2), typeof(TFilterAttribute3), typeof(TFilterAttribute4), typeof(TFilterAttribute5));
        }
        public void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5, TFilterAttribute6>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
            where TFilterAttribute6 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2), typeof(TFilterAttribute3), typeof(TFilterAttribute4), typeof(TFilterAttribute5), typeof(TFilterAttribute6));
        }
        public void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5, TFilterAttribute6, TFilterAttribute7>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
            where TFilterAttribute6 : FilterAttribute
            where TFilterAttribute7 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2), typeof(TFilterAttribute3), typeof(TFilterAttribute4), typeof(TFilterAttribute5), typeof(TFilterAttribute6), typeof(TFilterAttribute7));
        }
        public void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5, TFilterAttribute6, TFilterAttribute7, TFilterAttribute8>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
            where TFilterAttribute6 : FilterAttribute
            where TFilterAttribute7 : FilterAttribute
            where TFilterAttribute8 : FilterAttribute
        {
            Register(action, typeof(TFilterAttribute1), typeof(TFilterAttribute2), typeof(TFilterAttribute3), typeof(TFilterAttribute4), typeof(TFilterAttribute5), typeof(TFilterAttribute6), typeof(TFilterAttribute7), typeof(TFilterAttribute8));
        }

        public void Register<TController>(Expression<Action<TController>> action, params Type[] filterTypes) where TController : System.Web.Mvc.Controller
        {
            var controllerType = typeof(TController);
            var methodCall = action.Body as MethodCallExpression;
            List<FilterRegisterItem> registerItems;
            if (!_filterRegisterItems.TryGetValue(controllerType, out registerItems))
            {
                registerItems = new List<FilterRegisterItem>();
                _filterRegisterItems[controllerType] = registerItems;
            }
            registerItems.Add(new FilterRegisterItem(controllerType, new ReflectedActionDescriptor(methodCall.Method, methodCall.Method.Name,
                new ReflectedControllerDescriptor(typeof(TController))), filterTypes));

        }
        public void Register<TController, TFilterAttribute>(Expression<Action<TController>> action, Action<TFilterAttribute> configeFilter, params Type[] filterTypes)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute : FilterAttribute
        {
            var controllerType = typeof(TController);
            var methodCall = action.Body as MethodCallExpression;
            List<FilterRegisterItem> registerItems;
            if (!_filterRegisterItems.TryGetValue(controllerType, out registerItems))
            {
                registerItems = new List<FilterRegisterItem>();
                _filterRegisterItems[controllerType] = registerItems;
            }
            registerItems.Add(new FilterRegisterConfigureItem<TFilterAttribute>(controllerType,
                new ReflectedActionDescriptor(methodCall.Method, methodCall.Method.Name, new ReflectedControllerDescriptor(typeof(TController))), configeFilter, filterTypes));

        }
        public FilterInfo GetMatched(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            List<FilterRegisterItem> registerItems;
            FilterInfo filterInfo = new FilterInfo();
            if (_filterRegisterItems.TryGetValue(controllerContext.Controller.GetType(), out registerItems))
            {
                var filters = registerItems.Where(m => m.IsSameAction(actionDescriptor)).SelectMany(m => m.Filters()).OrderBy(m => m.Order);

                filters.OfType<IAuthorizationFilter>().Each(filterInfo.AuthorizationFilters.Add);

                filters.OfType<IActionFilter>().Each(filterInfo.ActionFilters.Add);

                filters.OfType<IResultFilter>().Each(filterInfo.ResultFilters.Add);

                filters.OfType<IExceptionFilter>().Each(filterInfo.ExceptionFilters.Add);
            }
            return filterInfo;
        }
    }
}