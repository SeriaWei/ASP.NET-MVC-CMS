using Easy.CMS.WidgetTemplate;
using Easy.CMS.Zone;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Easy.Constant;
using Easy.CMS.Widget;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme]
    public class WidgetController : Controller
    {
        public ActionResult Create(string PageID, long WidgetTemplateID)
        {
            var template = new WidgetTemplateService().Get(WidgetTemplateID);
            var widget = template.CreateWidgetInstance();
            widget.PageId = PageID;
            widget.Position = 1;
            ViewData[ViewDataKeys.Zones] = new ZoneService().GetZones(PageID).ToDictionary(m => m.ID, m => m.ZoneName);
            return View(widget);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(WidgetBase widget)
        {
            if (!ModelState.IsValid)
            {
                ViewData[ViewDataKeys.Zones] = new ZoneService().GetZones(widget.PageId).ToDictionary(m => m.ID, m => m.ZoneName);
                return View(widget);
            }
            widget.CreateServiceInstance().AddWidget(widget);
            return RedirectToAction("Design", "Page", new { module = "Common", ID = widget.PageId });
        }

        public ActionResult Edit(string ID)
        {
            var widgetService = new WidgetService();
            var widget = widgetService.Get(ID).CreateServiceInstance().GetWidget(ID);
            var zones = new Easy.CMS.Zone.ZoneService().GetZones(widget.PageId);
            ViewData[ViewDataKeys.Zones] = zones.ToDictionary(m => m.ID, m => m.ZoneName);
            return View(widget);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(WidgetBase widget)
        {
            if (!ModelState.IsValid)
            {
                var zones = new Easy.CMS.Zone.ZoneService().GetZones(widget.PageId);
                ViewData[ViewDataKeys.Zones] = zones.ToDictionary(m => m.ID, m => m.ZoneName);
                return View(widget);
            }
            widget.CreateServiceInstance().UpdateWidget(widget);
            return RedirectToAction("Design", "Page", new { module = "Common", ID = widget.PageId });
        }
        [HttpPost]
        public JsonResult SaveWidgetPosition(List<WidgetBase> widgets)
        {
            WidgetService widgetService = new WidgetService();
            widgets.Each(m =>
            {
                widgetService.Update(m, new Data.DataFilter(new List<string> { "Position" }).Where<WidgetBase>(n => n.ID, OperatorType.Equal, m.ID));
            });
            return Json(true);
        }
        [HttpPost]
        public JsonResult SaveWidgetZone(WidgetBase widget)
        {
            new WidgetService().Update(widget, new Data.DataFilter(new List<string> { "ZoneId", "Position" }).Where<WidgetBase>(n => n.ID, OperatorType.Equal, widget.ID));
            return Json(true);
        }
        [HttpPost]
        public JsonResult DeleteWidget(string ID)
        {
            WidgetService widgetService = new WidgetService();
            WidgetBase widget = widgetService.Get(ID);
            if (widget != null)
            {
                widget.CreateServiceInstance().DeleteWidget(ID);
                return Json(true);
            }
            return Json(false);
        }
    }
}
