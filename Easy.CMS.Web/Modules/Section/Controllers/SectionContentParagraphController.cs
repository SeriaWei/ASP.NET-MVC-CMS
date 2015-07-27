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
    public class SectionContentParagraphController : Controller
    {
        //
        // GET: /SectionContentTitle/

        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentParagraph
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                SectionContentType = (int)SectionContent.Types.Paragraph,
                ActionType = ActionType.Create
            });
        }

        public ActionResult Edit(int Id)
        {
            var content = new SectionContentParagraphService().Get(Id);
            content.ActionType = ActionType.Update;
            return View("Form", content);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Save(SectionContentParagraph content)
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
                new SectionContentParagraphService().Update(content);
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
