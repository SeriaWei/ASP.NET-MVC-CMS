using Easy.CMS.Common.Models;
using Easy.Web.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Common.Service;
using Easy.Web.Authorize;
using Easy.Web.CMS.Setting;

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
