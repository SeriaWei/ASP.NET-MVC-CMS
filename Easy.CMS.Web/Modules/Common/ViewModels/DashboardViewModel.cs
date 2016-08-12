using System.Collections.Generic;
using Easy.CMS.Common.Models;
using Easy.Web.CMS.Page;

namespace Easy.CMS.Common.ViewModels
{
    public class DashboardViewModel
    {
        public List<string> PageViewDate { get; set; }
        public List<int> PageViewCount { get; set; }
        public List<int> PageUniqueViewCount { get; set; }
        public List<int> PageIpAddressCount { get; set; } 
        public List<string> Layouts { get; set; }
        public List<int> LayoutUsage { get; set; }
        public IEnumerable<PageEntity> UnPublishPage { get; set; }
        public IEnumerable<PageView> CurrentTop { get; set; }
        public long Products { get; set; }
        public long Articles { get; set; }
        public long Medias { get; set; }
        public long Pages { get; set; }
    }
}