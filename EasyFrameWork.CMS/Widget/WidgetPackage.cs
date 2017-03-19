using Easy.Web.CMS.PackageManger;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.Widget
{
    public class WidgetPackage : FilePackage
    {
        public WidgetPackage(string installer) : base(installer)
        {

        }
        public WidgetBase Widget { get; set; }
    }
}
