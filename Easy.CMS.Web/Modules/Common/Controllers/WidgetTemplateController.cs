using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Data;
using Easy.Web.CMS;
using Easy.Web.CMS.WidgetTemplate;
using Easy.Web.Controller;
using Easy.Web.Attribute;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme]
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, WidgetTemplateService>
    {
        public WidgetTemplateController() : base(new WidgetTemplateService())
        {
        }

        public ActionResult SelectWidget(QueryContext context)
        {
            var viewModel = new WidgetTemplateViewModel
            {
                PageID = context.PageID,
                LayoutID = context.LayoutID,
                ZoneID = context.ZoneID,
                ReturnUrl = context.ReturnUrl,
                WidgetTemplates = Service.Get(new DataFilter().OrderBy("Order", OrderType.Ascending)).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult RedirectToWidget(QueryContext context)
        {
            return RedirectToAction("Create", "Widget", new { module = "Common", context.PageID, context.LayoutID, context.ZoneID, context.ReturnUrl, context.WidgetTemplateID });
        }
    }
}
