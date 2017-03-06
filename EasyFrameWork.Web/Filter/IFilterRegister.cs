/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Linq.Expressions;
using System.Web.Mvc;
using Easy.IOC;

namespace Easy.Web.Filter
{
    public interface IFilterRegister : IDependency, ISingleInstance
    {
        void Register<TController, TFilterAttribute>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute : FilterAttribute;

        void Register<TController, TFilterAttribute>(Expression<Action<TController>> action, Action<TFilterAttribute> configFilter)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute : FilterAttribute;

        void Register<TController, TFilterAttribute1, TFilterAttribute2>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute;

        void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3>(
            Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute;

        void Register<TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4>(
            Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute;

        void Register
            <TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5>
            (Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute;

        void Register
            <TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5,
                TFilterAttribute6>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
            where TFilterAttribute6 : FilterAttribute;

        void Register
            <TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5,
                TFilterAttribute6, TFilterAttribute7>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
            where TFilterAttribute6 : FilterAttribute
            where TFilterAttribute7 : FilterAttribute;

        void Register
            <TController, TFilterAttribute1, TFilterAttribute2, TFilterAttribute3, TFilterAttribute4, TFilterAttribute5,
                TFilterAttribute6, TFilterAttribute7, TFilterAttribute8>(Expression<Action<TController>> action)
            where TController : System.Web.Mvc.Controller
            where TFilterAttribute1 : FilterAttribute
            where TFilterAttribute2 : FilterAttribute
            where TFilterAttribute3 : FilterAttribute
            where TFilterAttribute4 : FilterAttribute
            where TFilterAttribute5 : FilterAttribute
            where TFilterAttribute6 : FilterAttribute
            where TFilterAttribute7 : FilterAttribute
            where TFilterAttribute8 : FilterAttribute;
        void Register<TController>(Expression<Action<TController>> action, params Type[] filterTypes)
            where TController : System.Web.Mvc.Controller;
        FilterInfo GetMatched(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
    }
}