using Easy.Web.CMS.Dashboard;

namespace Easy.CMS.Common.Service
{
    public class DashboardWelcomePartService : IDashboardPartDriveService
    {

        public DashboardPart Create()
        {
            return new DashboardPart
            {
                Order = -1,
                ViewName = "Dashboard.Welcome"
            };
        }
    }
}