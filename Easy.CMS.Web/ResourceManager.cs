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
            Script("bootStrap").Include("~/Content/bootstrap/js/bootstrap.js", "~/Content/bootstrap/js/bootstrap.min.js").RequiredAtFoot();
            Script("jQueryUi").Include("~/Scripts/jquery-ui-1.8.24.js", "~/Scripts/jquery-ui-1.8.24.min.js");
            Script("admin")
                .Include("~/Content/themes/admin/script/admin.js")
                .Include("~/Scripts/EasyPlug/Easy.js")
                .Include("~/Scripts/EasyPlug/Easy.Grid.js")
                .Include("~/Scripts/jstree/src/jstree.js")
                .Include("~/Scripts/jstree/src/jstree.checkbox.js")
                .Include("~/Scripts/jstree/src/jstree.contextmenu.js")
                .Include("~/Scripts/jstree/src/jstree.dnd.js")
                .Include("~/Scripts/jstree/src/jstree.search.js")
                .Include("~/Scripts/jstree/src/jstree.sort.js")
                .Include("~/Scripts/jstree/src/jstree.state.js")
                .Include("~/Scripts/jstree/src/jstree.types.js")
                .Include("~/Scripts/jstree/src/jstree.unique.js")
                .Include("~/Scripts/jstree/src/jstree.wholerow.js");
        }

        public override void InitStyle()
        {
            Style("bootStrap").Include("~/Content/bootstrap/css/bootstrap.css", "~/Content/bootstrap/css/bootstrap.min.css").RequiredAtHead();
            Style("bootStrapTheme").Include("~/Content/bootstrap/css/bootstrap-theme.css", "~/Content/bootstrap/css/bootstrap-theme.min.css").RequiredAtHead();
            Style("Site").Include("~/Content/Site.css").RequiredAtHead();
            Style("jQueryUi").Include("~/Content/themes/base/jquery-ui.css");
            Style("admin")
                .Include("~/Content/themes/admin/admin.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.Grid.css")
                .Include("~/Scripts/jstree/src/themes/default/style.css");
        }
    }
}