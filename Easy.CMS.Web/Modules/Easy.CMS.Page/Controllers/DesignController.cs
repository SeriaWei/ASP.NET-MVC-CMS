using Easy.CMS.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Easy.CMS.Filter;
using Easy.CMS.Widget;

namespace Easy.CMS.Page.Controllers
{
    public class DesignController : Controller
    {
        //
        // GET: /Layout/


        public ActionResult Layout(string layoutId)
        {
            LayoutEntity layout = null;
            if (!layoutId.IsNullOrEmpty())
            {
                layout = new LayoutService().Get(layoutId);
            }
            return View(layout ?? new LayoutEntity());
        }

        [EditWidget]
        public ActionResult Page()
        {
            return View();
        }
        [ValidateInput(false)]
        public ActionResult SaveLayout(string[] html, LayoutEntity layout)
        {
            LayoutHtmlCollection htmls;
            var zones = Easy.CMS.Zone.Helper.GetZones(html, out htmls);
            layout.Zones = zones;
            layout.Html = htmls;
            if (layout.LayoutId.IsNullOrEmpty())
            {
                new LayoutService().Add(layout);
            }
            else
            {
                new LayoutService().Update(layout);
            }
            return RedirectToAction("Layout", new { layoutId = layout.LayoutId });
        }
        public JsonResult SaveWidgetPosition(List<WidgetBase> widgets)
        {
            WidgetService widgetService = new WidgetService();
            widgets.Each(m =>
            {
                widgetService.Update(m, new Data.DataFilter(new List<string> { "Position" }).Where<WidgetBase>(n => n.WidgetId, Constant.DataEnumerate.OperatorType.Equal, m.WidgetId));
            });
            return Json(true);
        }
    }
}
