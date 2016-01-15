using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS
{
    public class AdminMenu
    {
        public string Title { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public int Order { get; set; }
        public IEnumerable<AdminMenu> Children { get; set; } 
    }
}
