using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Controller;
using Easy.CMS.Layout;
using Easy.Web.Attribute;

namespace Easy.CMS.Layout.Controllers
{
    [Admin]
    public class AdminController : BasicController<LayoutEntity, string, LayoutService>
    {
        public override System.Web.Mvc.ActionResult Index()
        {
            return View(Service.Get(new Data.DataFilter()));
        }
    }
}
