/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.IOC;

namespace Easy.Web.CMS.Dashboard
{
    public interface IDashboardProviderService : IDependency
    {
        IEnumerable<DashboardPart> GetDashboardParts();
    }
}