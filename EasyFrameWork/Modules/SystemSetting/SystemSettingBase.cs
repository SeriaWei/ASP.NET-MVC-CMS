using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Modules.SystemSetting
{
    public abstract class SystemSettingBase
    {
        public string SiteName { get; set; }
        public string SEOKeyWord { get; set; }
        public string SEODescription { get; set; }
        public string Power { get; set; }
        public string Host { get; set; }
    }
}
