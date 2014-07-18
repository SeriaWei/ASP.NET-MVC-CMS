using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlugWeb
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            Script("jQuery").Include("~/Scripts/jquery-1.8.2.js", "~/Scripts/jquery-1.8.2.min.js").RequiredAtHead();
            Script("bootStrap").Include("~/Content/bootstrap/js/bootstrap.js", "~/Content/bootstrap/js/bootstrap.min.js").RequiredAtHead();
            Script("jQueryUi").Include("~/Scripts/jquery-ui-1.8.24.js", "~/Scripts/jquery-ui-1.8.24.min.js");
        }

        public override void InitStyle()
        {
            Style("bootStrap").Include("~/Content/bootstrap/css/bootstrap.css", "~/Content/bootstrap/css/bootstrap.min.css").RequiredAtHead();
            Style("bootStrapTheme").Include("~/Content/bootstrap/css/bootstrap-theme.css", "~/Content/bootstrap/css/bootstrap-theme.min.css").RequiredAtHead();
            Style("Site").Include("~/Content/Site.css").RequiredAtHead();
            Style("jQueryUi").Include("~/Content/themes/base/jquery-ui.css");
        }
    }
}