using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Easy
{
    public class ResourceManager : Easy.Web.Resource.ResourceManager
    {
        const string CdnHost = "http://cdn.zkeasoft.com";
        protected override void InitScript(Func<string, Web.Resource.ResourceHelper> script)
        {

            script("jQuery").Include("~/Scripts/jquery-2.2.3.js", "~/Scripts/jquery-2.2.3.min.js", "//cdn.bootcss.com/jquery/2.2.3/jquery.js").RequiredAtHead();
            script("bootStrap").Include("~/Content/bootstrap/js/bootstrap.js", "~/Content/bootstrap/js/bootstrap.min.js", "//cdn.bootcss.com/bootstrap/3.3.6/js/bootstrap.min.js").RequiredAtFoot();
            script("jQueryUi")
                .Include("~/Scripts/jquery-ui/jquery-ui.js", "~/Scripts/jquery-ui/jquery-ui.min.js", "//cdn.bootcss.com/jqueryui/1.11.4/jquery-ui.min.js");

            script("Easy")
                .Include("~/Scripts/EasyPlug/Easy.js", "~/Scripts/EasyPlug/Easy.min.js", CdnHost + "/Scripts/EasyPlug/Easy.min.js").RequiredAtHead();

            script("lightBox")
                .Include("~/Scripts/lightbox/js/lightbox.js", "~/Scripts/lightbox/js/lightbox.js", CdnHost + "/Scripts/lightbox/js/lightbox.js");

            script("validate")
                .Include("~/Scripts/jquery.validate.js", "~/Scripts/jquery.validate.min.js", CdnHost + "/Scripts/jquery.validate.min.js")
                .Include("~/Scripts/jquery.validate.unobtrusive.js", "~/Scripts/jquery.validate.unobtrusive.min.js", CdnHost + "/Scripts/jquery.validate.unobtrusive.min.js")
                .Include("~/Scripts/jquery.unobtrusive-ajax.js", "~/Scripts/jquery.unobtrusive-ajax.min.js", CdnHost + "/Scripts/jquery.unobtrusive-ajax.min.js");

            script("jsTree")
                .Include("~/Scripts/jstree/src/jstree.js", "~/Scripts/jstree/src/jstree.js", CdnHost + "/Scripts/jstree/src/jstree.js")
                .Include("~/Scripts/jstree/src/jstree.checkbox.js", "~/Scripts/jstree/src/jstree.checkbox.min.js", CdnHost + "/Scripts/jstree/src/jstree.checkbox.min.js")
                .Include("~/Scripts/jstree/src/jstree.contextmenu.js", "~/Scripts/jstree/src/jstree.contextmenu.min.js", CdnHost + "/Scripts/jstree/src/jstree.contextmenu.min.js")
                .Include("~/Scripts/jstree/src/jstree.dnd.js", "~/Scripts/jstree/src/jstree.dnd.min.js", CdnHost + "/Scripts/jstree/src/jstree.dnd.min.js")
                .Include("~/Scripts/jstree/src/jstree.search.js", "~/Scripts/jstree/src/jstree.search.min.js", CdnHost + "/Scripts/jstree/src/jstree.search.min.js")
                .Include("~/Scripts/jstree/src/jstree.sort.js", "~/Scripts/jstree/src/jstree.sort.min.js", CdnHost + "/Scripts/jstree/src/jstree.sort.min.js")
                .Include("~/Scripts/jstree/src/jstree.state.js", "~/Scripts/jstree/src/jstree.state.min.js", CdnHost + "/Scripts/jstree/src/jstree.state.min.js")
                .Include("~/Scripts/jstree/src/jstree.types.js", "~/Scripts/jstree/src/jstree.types.min.js", CdnHost + "/Scripts/jstree/src/jstree.types.min.js")
                .Include("~/Scripts/jstree/src/jstree.unique.js", "~/Scripts/jstree/src/jstree.unique.min.js", CdnHost + "/Scripts/jstree/src/jstree.unique.min.js")
                .Include("~/Scripts/jstree/src/jstree.wholerow.js", "~/Scripts/jstree/src/jstree.wholerow.min.js", CdnHost + "/Scripts/jstree/src/jstree.wholerow.min.js");

            
            script("admin")
                .Include("~/Content/themes/Admin/script/admin.js", "~/Content/themes/Admin/script/admin.min.js", CdnHost + "/Content/themes/Admin/script/admin.min.js")
                .Include("~/Scripts/EasyPlug/Easy.Grid.js", "~/Scripts/EasyPlug/Easy.Grid.min.js", CdnHost + "/Scripts/EasyPlug/Easy.Grid.min.js")
                .Include("~/Scripts/tinymce/tinymce.min.js", "~/Scripts/tinymce/tinymce.min.js")
                .Include("~/Scripts/CryptoJS/components/core.js", "~/Scripts/CryptoJS/components/core-min.js", CdnHost + "/Scripts/CryptoJS/components/core-min.js")
                .Include("~/Scripts/CryptoJS/components/enc-base64.js", "~/Scripts/CryptoJS/components/enc-base64-min.js", CdnHost + "/Scripts/CryptoJS/components/enc-base64-min.js")
                .Include("~/Scripts/Statistics.js")
                .Include("~/Scripts/bootstrap-datetimepicker/moment-with-locales.js", "~/Scripts/bootstrap-datetimepicker/moment-with-locales.min.js", CdnHost + "/Scripts/bootstrap-datetimepicker/moment-with-locales.min.js")
                .Include("~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.js", "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js", CdnHost + "/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.js");

        }

        protected override void InitStyle(Func<string, Web.Resource.ResourceHelper> style)
        {
            style("bootStrap")
                .Include("~/Content/bootstrap/css/bootstrap.css", "~/Content/bootstrap/css/bootstrap.min.css", "//cdn.bootcss.com/bootstrap/3.3.6/css/bootstrap.css")
                .Include("~/Content/bootstrap/css/bootstrap-theme.css", "~/Content/bootstrap/css/bootstrap-theme.min.css", "//cdn.bootcss.com/bootstrap/3.3.6/css/bootstrap-theme.min.css");

            style("Site").Include("~/Content/Site.css", "~/Content/Site.min.css", CdnHost + "/Content/Site.min.css").RequiredAtFoot();
            style("jQueryUi")
                .Include("~/Scripts/jquery-ui/jquery-ui.css", "~/Scripts/jquery-ui/jquery-ui.min.css", "//cdn.bootcss.com/jqueryui/1.11.4/jquery-ui.min.css")
                .Include("~/Scripts/jquery-ui/jquery-ui.theme.css", "~/Scripts/jquery-ui/jquery-ui.theme.min.css", "//cdn.bootcss.com/jqueryui/1.11.4/jquery-ui.theme.min.css")
                .Include("~/Scripts/jquery-ui/jquery-ui.structure.css", "~/Scripts/jquery-ui/jquery-ui.structure.css", "//cdn.bootcss.com/jqueryui/1.11.4/jquery-ui.structure.min.css");
            style("Easy")
                .Include("~/Scripts/EasyPlug/Css/Easy.css", "~/Scripts/EasyPlug/Css/Easy.min.css", CdnHost + "/Scripts/EasyPlug/Css/Easy.min.css");

            style("lightBox")
               .Include("~/Scripts/lightbox/css/lightbox.css", "~/Scripts/lightbox/css/lightbox.min.css", CdnHost + "/Scripts/lightbox/css/lightbox.min.css");

            style("admin")
                .Include("~/Content/themes/Admin/admin.css", "~/Content/themes/Admin/admin.min.css", CdnHost + "/Content/themes/Admin/admin.min.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.css", "~/Scripts/EasyPlug/Css/Easy.min.css", CdnHost + "/Scripts/EasyPlug/Css/Easy.min.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.Grid.css", "~/Scripts/EasyPlug/Css/Easy.Grid.min.css", CdnHost + "/Scripts/EasyPlug/Css/Easy.Grid.min.css")
                .Include("~/Scripts/EasyPlug/Css/Easy.UI.css", "~/Scripts/EasyPlug/Css/Easy.UI.min.css", CdnHost + "/Scripts/EasyPlug/Css/Easy.UI.min.css")
                .Include("~/Scripts/jstree/src/themes/default/style.css", "~/Scripts/jstree/src/themes/default/style.min.css", CdnHost + "/Scripts/jstree/src/themes/default/style.min.css")
                .Include("~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.css", "~/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css", CdnHost + "/Scripts/bootstrap-datetimepicker/bootstrap-datetimepicker.min.css");
        }
    }
}