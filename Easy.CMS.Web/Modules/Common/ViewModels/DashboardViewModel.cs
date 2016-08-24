using System.Collections.Generic;
using Easy.CMS.Common.Models;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Chart;

namespace Easy.CMS.Common.ViewModels
{
    public class DashboardViewModel
    {
        public IEnumerable<ChartDescriptor> Charts { get; set; }
        public IEnumerable<PageEntity> UnPublishPage { get; set; }
        //public IEnumerable<PageView> CurrentTop { get; set; }
        public long Products { get; set; }
        public long Articles { get; set; }
        public long Medias { get; set; }
        public long Pages { get; set; }
    }
}