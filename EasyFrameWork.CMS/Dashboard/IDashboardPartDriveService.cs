/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;

namespace Easy.Web.CMS.Dashboard
{
    public interface IDashboardPartDriveService : IDependency
    {
        DashboardPart Create();
    }
}