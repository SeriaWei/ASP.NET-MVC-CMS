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
using Easy.Web.Authorize;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize]
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, long, IWidgetTemplateService>
    {
        public WidgetTemplateController(IWidgetTemplateService widgetTemplateService)
            : base(widgetTemplateService)
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
