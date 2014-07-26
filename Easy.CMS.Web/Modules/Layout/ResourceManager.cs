using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Layout
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            
        }

        public override void InitStyle()
        {
            Style("Design").Include("~/Modules/Page/Content/Design.css");
        }
    }
}