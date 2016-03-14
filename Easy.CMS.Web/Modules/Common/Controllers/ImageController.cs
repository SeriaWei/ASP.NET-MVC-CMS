using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme,Authorize]
    public class ImageController : Controller
    {
        //
        // GET: /Image/

        public ActionResult Index()
        {
            return View();
        }

    }
}
