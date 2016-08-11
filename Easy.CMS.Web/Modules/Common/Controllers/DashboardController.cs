using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;
using Easy.Web.Authorize;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/

        public ActionResult Index()
        {
            return View();
        }

    }
}
