using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Section.Models;
using Easy.CMS.Section.Service;
using Easy.Constant;
using Easy.Data;
using Easy.Extend;
using Easy.Web.Attribute;

namespace Easy.CMS.Section.Controllers
{
    [PopUp, Authorize]
    public class SectionGroupController : Controller
    {
        private readonly SectionGroupService _sectionGroupService;
        public SectionGroupController()
        {
            _sectionGroupService = new SectionGroupService();
        }

        public ActionResult Create(string sectionWidgetId)
        {
            return View("Form", new SectionGroup { 
                SectionWidgetId = sectionWidgetId, 
                ActionType = ActionType.Create,
                Order = _sectionGroupService.Get("SectionWidgetId", OperatorType.Equal, sectionWidgetId).Count() + 1
            });
        }
        public ActionResult Edit(int Id)
        {
            var group = _sectionGroupService.Get(Id);
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
                _sectionGroupService.Add(group);
            }
            else
            {
                _sectionGroupService.Update(group);
            }
            ViewBag.Close = true;
            return View("Form", group);
        }

        public JsonResult Delete(int Id)
        {
            _sectionGroupService.Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult Sort(List<SectionGroup> groups)
        {
            groups.Each(m =>
            {
                var g = _sectionGroupService.Get(m.ID);
                g.Order = m.Order;
                _sectionGroupService.Update(g);
            });
            return Json(true);
        }

        [HttpPost]
        public JsonResult SortContent(List<SectionContent> contents)
        {
            var contentService = new SectionContentService();
            contents.Each(m =>
            {
                var g = contentService.Get(m.ID);
                g.Order = m.Order;
                contentService.Update(g);
            });
            return Json(true);
        }
    }
}
