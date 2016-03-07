using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        public override void InitScript()
        {
            Script("jQuery").Include("~/Scripts/jquery-1.11.2.min.js", "~/Scripts/jquery-1.11.2.min.js").RequiredAtHead();
            Script("bootStrap").Include("~/Content/bootstrap/js/bootstrap.js", "~/Content/bootstrap/js/bootstrap.min.js").RequiredAtFoot();
            Script("jQueryUi")
                .Include("~/Scripts/jquery-ui/jquery-ui.js", "~/Scripts/jquery-ui/jquery-ui.min.js");

            Script("Easy")
                .Include("~/Scripts/EasyPlug/Easy.js").RequiredAtHead();

            Script("lightBox")
                .Include("~/Scripts/lightbox/js/lightbox.js");

            Script("admin")
                .Include("~/Scripts/jquery.validate.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js")
                .Include("~/Content/themes/admin/script/admin.js")
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
                .Include("~/Scripts/jstree/src/jstree.wholerow.js")
                .Include("~/Scripts/tinymce/tinymce.min.js")
                .Include("~/Scripts/CryptoJS/components/core.js", "~/Scripts/CryptoJS/components/core-min.js")
                .Include("~/Scripts/CryptoJS/components/enc-base64.js", "~/Scripts/CryptoJS/components/enc-base64-min.js")
                .Include("~/Scripts/Statistics.js")
                .Include("~/Scripts/bootstrap-datetimepicker/moment-with-locales.js")
                .Include("~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js", "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js");

        }

        public override void InitStyle()
        {
            Style("bootStrap").Include("~/Content/bootstrap/css/bootstrap.css", "~/Content/bootstrap/css/bootstrap.min.css").RequiredAtHead();
            Style("bootStrapTheme").Include("~/Content/bootstrap/css/bootstrap-theme.css", "~/Content/bootstrap/css/bootstrap-theme.min.css").RequiredAtHead();
            Style("Site").Include("~/Content/Site.css", "~/Content/Site.min.css").RequiredAtFoot();
            Style("jQueryUi")
                .Include("~/Scripts/jquery-ui/jquery-ui.css", "~/Scripts/jquery-ui/jquery-ui.min.css")
                .Include("~/Scripts/jquery-ui/jquery-ui.theme.css", "~/Scripts/jquery-ui/jquery-ui.theme.min.css")
                .Include("~/Scripts/jquery-ui/jquery-ui.structure.css", "~/Scripts/jquery-ui/jquery-ui.structure.css");
            Style("Easy")
                .Include("~/Scripts/EasyPlug/Css/Easy.css");

            Style("lightBox")
               .Include("~/Scripts/lightbox/css/lightbox.css");

            Style("admin")
                .Include("~/Content/themes/admin/admin.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.Grid.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.UI.css")
                .Include("~/Scripts/jstree/src/themes/default/style.css")
                .Include("~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.css", "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css");
        }
    }
}