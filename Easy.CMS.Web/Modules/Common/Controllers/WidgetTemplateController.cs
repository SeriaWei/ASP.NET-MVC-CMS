using System.Linq;
using System.Web.Mvc;
using Easy.Data;
using Easy.Extend;
using Easy.Web.Attribute;
using Easy.Web.Authorize;
using Easy.Web.CMS;
using Easy.Web.CMS.WidgetTemplate;
using Easy.Web.Controller;
using Easy.Web.ValueProvider;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, DefaultAuthorize(PermissionKeys.ManagePage)]
    public class WidgetTemplateController : BasicController<WidgetTemplateEntity, long, IWidgetTemplateService>
    {
        private readonly ICookie _cookie;
        public WidgetTemplateController(IWidgetTemplateService widgetTemplateService, ICookie cookie)
            : base(widgetTemplateService)
        {
            _cookie = cookie;
        }

        public ActionResult SelectWidget(QueryContext context)
        {
            var viewModel = new WidgetTemplateViewModel
            {
                PageID = context.PageID,
                LayoutID = context.LayoutID,
                ZoneID = context.ZoneID,
                ReturnUrl = context.ReturnUrl,
                CanPasteWidget = context.ZoneID.IsNotNullAndWhiteSpace() && _cookie.GetValue<string>(Const.CopyWidgetCookie).IsNotNullAndWhiteSpace(),
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
