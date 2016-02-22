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
    [AdminTheme, Authorize]
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, long, WidgetTemplateService>
    {
        public WidgetTemplateController()
            : base(new WidgetTemplateService())
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
                WidgetTemplates = Service.Get(new DataFilter().OrderBy("[Order]", OrderType.Ascending)).ToList()
            };
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult RedirectToWidget(QueryContext context)
        {
            return RedirectToAction("Create", "Widget", new { module = "admin", context.PageID, context.LayoutID, context.ZoneID, context.WidgetTemplateID, context.ReturnUrl });
        }
    }
}
