using System.Collections.Generic;
using Easy.IOC;

namespace Easy.Web.CMS.Dashboard
{
    public interface IDashboardProviderService : IDependency
    {
        IEnumerable<DashboardPart> GetDashboardParts();
    }
}