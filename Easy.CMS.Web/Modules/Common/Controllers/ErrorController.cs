using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Common.Controllers
{
    
    public class ErrorController : Controller
    {
        [HandleError]
        public ActionResult NoPage()
        {
            return View();
        }

    }
}
