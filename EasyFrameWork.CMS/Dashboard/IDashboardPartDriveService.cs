using Easy.IOC;

namespace Easy.Web.CMS.Dashboard
{
    public interface IDashboardPartDriveService : IDependency
    {
        DashboardPart Create();
    }
}