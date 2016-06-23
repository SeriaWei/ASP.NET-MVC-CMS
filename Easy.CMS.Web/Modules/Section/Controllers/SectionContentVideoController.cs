using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.Section.Models;
using Easy.CMS.Section.Service;
using Easy.Constant;
using Easy.Data;
using Easy.Web.Attribute;

namespace Easy.CMS.Section.Controllers
{
    public class SectionContentVideoController : Controller
    {
        private readonly ISectionContentProviderService _sectionContentProviderService;

        public SectionContentVideoController(ISectionContentProviderService sectionContentProviderService)
        {
            _sectionContentProviderService = sectionContentProviderService;
        }
        [PopUp,Authorize]
        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentVideo
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                ActionType = ActionType.Create
            });
        }
        [PopUp, Authorize]
        public ActionResult Edit(int Id)
        {
            var content = _sectionContentProviderService.Get(Id);
            content.ActionType = ActionType.Update;
            return View("Form", content);
        }
        [PopUp, HttpPost, ValidateInput(false), Authorize]
        public ActionResult Save(SectionContentVideo content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                _sectionContentProviderService.Add(content);
            }
            else
            {
                _sectionContentProviderService.Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }
        [Authorize]
        public JsonResult Delete(int Id)
        {
            _sectionContentProviderService.Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Play(int Id)
        {
            return View(_sectionContentProviderService.Get(Id));
        }
    }
}
