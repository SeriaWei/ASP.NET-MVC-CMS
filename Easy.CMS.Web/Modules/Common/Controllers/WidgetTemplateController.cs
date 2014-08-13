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
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, long, WidgetTemplateService>
    {
        public ActionResult SelectWidget(string pageId, string LayoutId)
        {
            WidgetTemplateViewModel viewModel = new WidgetTemplateViewModel();
            viewModel.PageId = pageId;
            viewModel.LayoutId = LayoutId;
            viewModel.WidgetTemplates = Service.Get(new Data.DataFilter().OrderBy("Order", Constant.OrderType.Ascending)).ToList();
            return View(viewModel);
        }
        [HttpPost]
        public ActionResult SelectWidget(string PageId, string LayoutId, long WidgetTemplateID)
        {
            return RedirectToAction("Create", "Widget", new { module = "Common", PageId, LayoutId, WidgetTemplateID });
        }
    }
}
