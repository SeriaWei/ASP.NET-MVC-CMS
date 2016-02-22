using Easy.Data;
using Easy.Web.CMS;
using Easy.Web.CMS.WidgetTemplate;
using Easy.Web.CMS.Zone;
using Easy.Web.Attribute;
using Easy.Web.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Easy.Constant;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Common.Controllers
{
    [AdminTheme, Authorize]
    public class WidgetController : Controller
    {
        [ViewData_Zones]
        public ActionResult Create(QueryContext context)
        {
            var template = new WidgetTemplateService().Get(context.WidgetTemplateID);
            var widget = template.CreateWidgetInstance();
            widget.PageID = context.PageID;
            widget.LayoutID = context.LayoutID;
            widget.ZoneID = context.ZoneID;
            widget.FormView = template.FormView;
            if (widget.PageID.IsNotNullAndWhiteSpace())
            {
                widget.Position =
                    new WidgetService().GetAllByPageId(context.PageID).Count(m => m.ZoneID == context.ZoneID) + 1;
            }
            else
            {
                widget.Position = new WidgetService().GetByLayoutId(context.LayoutID).Count(m => m.ZoneID == context.ZoneID) + 1;
            }
            ViewBag.ReturnUrl = context.ReturnUrl;
            if (template.FormView.IsNotNullAndWhiteSpace())
            {
                return View(template.FormView, widget);
            }
            return View(widget);
        }
        [HttpPost, ViewData_Zones]
        [ValidateInput(false)]
        public ActionResult Create(WidgetBase widget, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(widget);
            }
            widget.CreateServiceInstance().AddWidget(widget);
            if (widget.ActionType == ActionType.Continue)
            {
                return RedirectToAction("Edit", new { widget.ID, ReturnUrl });
            }
            else if (!ReturnUrl.IsNullOrEmpty())
            {
                return Redirect(ReturnUrl);
            }
            else if (!widget.PageID.IsNullOrEmpty())
            {
                return RedirectToAction("Design", "Page", new { module = "admin", ID = widget.PageID });
            }
            else
            {
                return RedirectToAction("LayoutWidget", "Layout", new { module = "admin" });
            }
        }
        [ViewData_Zones]
        public ActionResult Edit(string ID, string ReturnUrl)
        {
            var widgetService = new WidgetService();
            var widgetBase = widgetService.Get(ID);
            var widget = widgetBase.CreateServiceInstance().GetWidget(widgetBase);
            ViewBag.ReturnUrl = ReturnUrl;
            if (widget.FormView.IsNotNullAndWhiteSpace())
            {
                return View(widget.FormView, widget);
            }
            return View(widget);
        }

        [HttpPost, ViewData_Zones]
        [ValidateInput(false)]
        public ActionResult Edit(WidgetBase widget, string ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(widget);
            }
            widget.CreateServiceInstance().UpdateWidget(widget);
            if (!ReturnUrl.IsNullOrEmpty())
            {
                return Redirect(ReturnUrl);
            }
            if (!widget.PageID.IsNullOrEmpty())
            {
                return RedirectToAction("Design", "Page", new { module = "admin", ID = widget.PageID });
            }
            else
            {
                return RedirectToAction("LayoutWidget", "Layout", new { module = "admin" });
            }
        }
        [HttpPost]
        public JsonResult SaveWidgetPosition(List<WidgetBase> widgets)
        {
            var widgetService = new WidgetService();
            widgets.Each(m =>
            {
                widgetService.Update(m, new Data.DataFilter(new List<string> { "Position" }).Where("ID", OperatorType.Equal, m.ID));
            });
            return Json(true);
        }
        [HttpPost]
        public JsonResult SaveWidgetZone(WidgetBase widget)
        {
            new WidgetService().Update(widget, new Data.DataFilter(new List<string> { "ZoneID", "Position" }).Where("ID", OperatorType.Equal, widget.ID));
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
