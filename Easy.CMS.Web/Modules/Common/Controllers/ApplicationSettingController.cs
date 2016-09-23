/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS.Setting;
using Easy.Web.Controller;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize(PermissionKey = PermissionKeys.ManageApplicationSetting)]
    public class ApplicationSettingController : BasicController<ApplicationSetting, string, IApplicationSettingService>
    {
        public ApplicationSettingController(IApplicationSettingService service)
            : base(service)
        {

        }
    }
}
