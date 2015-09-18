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
        private readonly ISectionWidgetService _sectionWidgetService;

        public SectionWidgetController(ISectionWidgetService sectionWidgetService)
        {
            _sectionWidgetService = sectionWidgetService;
        }

        public ActionResult Editor(string sectionWidgetId)
        {
            return View(_sectionWidgetService.Get(sectionWidgetId));
        }

    }
}
