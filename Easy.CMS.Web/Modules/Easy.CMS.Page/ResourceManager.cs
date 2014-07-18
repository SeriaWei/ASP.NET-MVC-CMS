using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugWeb.Page
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            
        }

        public override void InitStyle()
        {
            Style("Design").Include("~/Modules/Easy.CMS.Page/Content/Design.css");
        }
    }
}