/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.CMS.Common.ViewModels;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Dashboard;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class DashboardController : Controller
    {
        private readonly IDashboardProviderService _dashboardProviderService;
        public DashboardController(IDashboardProviderService dashboardProviderService)
        {
            _dashboardProviderService = dashboardProviderService;
        }

        public ActionResult Index()
        {
            return View(new DashboardViewModel { Parts = _dashboardProviderService.GetDashboardParts() });
        }

    }
}
