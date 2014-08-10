using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy.CMS.Common
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            Script("Navigation").Include("~/Modules/Common/Scripts/Navigation.js");
        }

        public override void InitStyle()
        {
            Style("Layout").Include("~/Modules/Common/Content/Layout.css");
            Style("Navigation").Include("~/Modules/Common/Content/Navigation.css");
        }
    }
}