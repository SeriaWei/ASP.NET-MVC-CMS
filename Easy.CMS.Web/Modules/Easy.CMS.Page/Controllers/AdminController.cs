using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;

namespace Easy.CMS.Page.Controllers
{
    [Admin]
    public class AdminController : Controller
    {
        //
        // GET: /Admin/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit(string pageId)
        {
            return View("Form");
        }
    }
}
