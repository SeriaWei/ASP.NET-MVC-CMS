/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Mvc.Async;
using Easy.Extend;
using Easy.Web.Filter;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.ActionInvoker
{
    public class FiltersAsyncControllerActionInvoker : AsyncControllerActionInvoker
    {
        protected override FilterInfo GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            var filterInfos = new List<FilterInfo> { base.GetFilters(controllerContext, actionDescriptor) };
            filterInfos.AddRange(ServiceLocator.Current.GetAllInstances<IConfigureFilter>()
                .Select(m => m.Registry.GetMatched(controllerContext, actionDescriptor)));
            var filterInfo = new FilterInfo();
            filterInfos.Each(m =>
            {
                m.ActionFilters.Each(filterInfo.ActionFilters.Add);
                m.AuthorizationFilters.Each(filterInfo.AuthorizationFilters.Add);
                m.ExceptionFilters.Each(filterInfo.ExceptionFilters.Add);
                m.ResultFilters.Each(filterInfo.ResultFilters.Add);
            });
            return filterInfo;
        }
    }
}