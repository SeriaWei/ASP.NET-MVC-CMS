using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.WidgetTemplate;
using Easy.Web.Controller;
using Easy.Web.Attribute;

namespace Easy.CMS.Widget.Controllers
{
    [AdminTheme]
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, long, WidgetTemplateService>
    {
        public ActionResult SelectWidget(string pageId)
        {
            WidgetTemplateViewModel viewModel = new WidgetTemplateViewModel();
            viewModel.PageID = pageId;
            viewModel.WidgetTemplates = Service.Get(new Data.DataFilter().OrderBy("Order", Constant.OrderType.Ascending));
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SelectWidget(string PageID, long WidgetTemplateID)
        {
            return RedirectToAction("Create", "Widget", new { module = "widget", PageID, WidgetTemplateID });
        }
    }
}
