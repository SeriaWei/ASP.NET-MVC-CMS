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
    [PopUp, Authorize]
    public class SectionContentCallToActionController : Controller
    {
        //
        // GET: /SectionContentTitle/

        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentCallToAction
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                SectionContentType = (int)SectionContent.Types.CallToAction,
                ActionType = ActionType.Create
            });
        }

        public ActionResult Edit(int Id)
        {
            var content = new SectionContentCallToActionService().Get(Id);
            content.ActionType = ActionType.Update;
            return View("Form", content);
        }
        [HttpPost]
        public ActionResult Save(SectionContentCallToAction content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                new SectionContentService().Add(content);
            }
            else
            {
                new SectionContentCallToActionService().Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }

        public JsonResult Delete(int Id)
        {
            new SectionContentService().Delete(Id);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
