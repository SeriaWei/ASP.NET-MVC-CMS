using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS
{
    public class AdminMenu
    {
        public IEnumerable<AdminMenu> Children { get; set; } 
    }
}
