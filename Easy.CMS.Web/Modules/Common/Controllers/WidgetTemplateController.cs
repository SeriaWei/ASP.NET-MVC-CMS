using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.CMS.WidgetTemplate;
using Easy.Web.Controller;
using Easy.Web.Attribute;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme]
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, WidgetTemplateService>
    {
        public ActionResult SelectWidget(QueryContext context)
        {
            WidgetTemplateViewModel viewModel = new WidgetTemplateViewModel
            {
                PageID = context.PageID,
                LayoutID = context.LayoutID,
                ZoneID = context.ZoneID,
                ReturnUrl = context.ReturnUrl,
                WidgetTemplates = Service.Get(new Data.DataFilter().OrderBy("Order", Constant.OrderType.Ascending)).ToList()
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
