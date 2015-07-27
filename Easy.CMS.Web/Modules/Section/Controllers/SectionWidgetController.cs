using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Section.Service;

namespace Easy.CMS.Section.Controllers
{
    [Authorize]
    public class SectionWidgetController : Controller
    {
        //
        // GET: /SectionWidget/

        public ActionResult Editor(string sectionWidgetId)
        {
            return View(new SectionWidgetService().Get(sectionWidgetId));
        }

    }
}
