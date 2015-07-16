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
        [HttpPost]
        public ActionResult Save(SectionContentCallToAction content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                new SectionContentCallToActionService().Add(content);
                content.SectionContentId = content.ID;
                new SectionContentService().Add(content.ToBaseContent());
            }
            else
            {
                new SectionContentCallToActionService().Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }
    }
}
