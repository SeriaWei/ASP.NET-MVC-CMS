using System.Collections.Generic;
using Easy.CMS.Common.Models;
using Easy.Web.CMS.Dashboard;
using Easy.Web.CMS.Page;

namespace Easy.CMS.Common.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<DashboardPart> Parts { get; set; }
    }
}