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
        [HttpPost, ValidateInput(false)]
        public ActionResult Save(SectionContentParagraph content)
        {
            if (!ModelState.IsValid)
            {
                return View("Form", content);
            }
            if (content.ActionType == ActionType.Create)
            {
                new SectionContentParagraphService().Add(content);
                content.SectionContentId = content.ID;
                new SectionContentService().Add(content.ToBaseContent());
            }
            else
            {
                new SectionContentParagraphService().Update(content);
            }
            ViewBag.Close = true;
            return View("Form", content);
        }
    }
}
