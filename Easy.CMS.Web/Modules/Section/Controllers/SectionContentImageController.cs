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
    public class SectionContentImageController : Controller
    {
        //
        // GET: /SectionContentTitle/

        public ActionResult Create(int sectionGroupId, string sectionWidgetId)
        {
            return View("Form", new SectionContentImage
            {
                SectionGroupId = sectionGroupId,
                SectionWidgetId = sectionWidgetId,
                SectionContentType = (int)SectionContent.Types.Image,
                ActionType = ActionType.Create
            });
        }
        [HttpPost]
        public ActionResult Save(SectionContentImage content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                new SectionContentImageService().Add(content);
                content.SectionContentId = content.ID;
                new SectionContentService().Add(content.ToBaseContent());
            }
            else
            {
                new SectionContentImageService().Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }
    }
}
