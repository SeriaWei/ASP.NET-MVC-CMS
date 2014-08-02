using Easy.CMS.Filter;
using Easy.Web.Route;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Page.Controllers
{
    public class PageController : Controller
    {
        [Widget]
        public ActionResult PreView()
        {
            return View();
        }

    }
}
