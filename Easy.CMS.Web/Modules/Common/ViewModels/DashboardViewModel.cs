/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using Easy.Web.CMS.Dashboard;

namespace Easy.CMS.Common.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<DashboardPart> Parts { get; set; }
    }
}