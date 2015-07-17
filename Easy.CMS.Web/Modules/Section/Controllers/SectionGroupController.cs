using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Section.Models;
using Easy.CMS.Section.Service;
using Easy.Constant;
using Easy.Web.Attribute;

namespace Easy.CMS.Section.Controllers
{
    [PopUp]
    public class SectionGroupController : Controller
    {
        //
        // GET: /SectionGroup/

        public ActionResult Create(string sectionWidgetId)
        {
            return View("Form", new SectionGroup { SectionWidgetId = sectionWidgetId, ActionType = ActionType.Create });
        }
        public ActionResult Edit(int Id)
        {
            var group = new SectionGroupService().Get(Id);
            group.ActionType = ActionType.Update;
            return View("Form", group);
        }
        [HttpPost]
        public ActionResult Save(SectionGroup group)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", group);
            }
            if (group.ActionType == ActionType.Create)
            {
                new SectionGroupService().Add(group);
            }
            else
            {
                new SectionGroupService().Update(group);
            }
            ViewBag.Close = true;
            return View("Form", group);
        }

        public JsonResult Delete(int Id)
        {
            new SectionGroupService().Delete(Id);
            return Json(true);
        }
    }
}
