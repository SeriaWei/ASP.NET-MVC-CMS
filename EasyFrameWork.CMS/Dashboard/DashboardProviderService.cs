/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Dashboard
{
    public class DashboardProviderService : IDashboardProviderService
    {

        public IEnumerable<DashboardPart> GetDashboardParts()
        {
            var partDrives = ServiceLocator.Current.GetAllInstances<IDashboardPartDriveService>().ToList();
            if (!partDrives.Any()) return new List<DashboardPart>();
            return partDrives.Select(p => p.Create());
        }
    }
}