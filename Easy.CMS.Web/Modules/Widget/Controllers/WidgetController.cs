using Easy.CMS.WidgetTemplate;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Easy.CMS.Widget.Controllers
{
    [AdminTheme]
    public class WidgetController : Controller
    {
        public ActionResult Create(string PageID, long WidgetTemplateID)
        {
            var template = new WidgetTemplateService().Get(WidgetTemplateID);
            var widget = template.CreateWidgetInstance();
            widget.PageId = PageID;
            var page = new Easy.CMS.Page.PageService().Get(PageID);
            var layout = new Easy.CMS.Layout.LayoutService().Get(page.LayoutId);
            var zones = new Easy.CMS.Zone.ZoneService().Get(new Data.DataFilter().Where("LayoutId", Constant.OperatorType.Equal, layout.ID));
            ViewData[ViewDataKeys.Zones] = zones.ToDictionary(m => m.ID, m => m.ZoneName);
            return View(widget);
        }
        [HttpPost]
        public ActionResult Create(WidgetBase widget)
        {
            widget.CreateServiceInstance().Add<WidgetBase>(widget);
            var model = Easy.Reflection.ClassAction.GetModel(widget.GetViewModelType(), Request.Form);
            return View();
        }
    }
}
