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
    [PopUp]
    public class SectionContentTitleController : Controller
    {
        //
        // GET: /SectionContentTitle/

        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentTitle
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                SectionContentType = (int)SectionContent.Types.Title,
                ActionType = ActionType.Create
            });
        }

        public ActionResult Edit(int Id)
        {
            var content = new SectionContentTitleService().Get(Id);
            content.ActionType = ActionType.Update;
            return View("Form", content);
        }
        [HttpPost]
        public ActionResult Save(SectionContentTitle content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                new SectionContentTitleService().Add(content);
                content.SectionContentId = content.ID;
                new SectionContentService().Add(content.ToBaseContent());
            }
            else
            {
                new SectionContentTitleService().Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }

        public JsonResult Delete(int Id)
        {
            new SectionContentTitleService().Delete(Id);
            new SectionContentService().Delete(new DataFilter()
                .Where("SectionContentId", OperatorType.Equal, Id)
                .Where("SectionContentType", OperatorType.Equal, (int)SectionContent.Types.Title));
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
