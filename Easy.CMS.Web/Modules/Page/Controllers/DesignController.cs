using Easy.CMS.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Easy.CMS.Filter;
using Easy.CMS.Widget;
using Easy.Constant;

namespace Easy.CMS.Page.Controllers
{
    public class DesignController : Controller
    {
        //
        // GET: /Layout/


        [EditWidget]
        public ActionResult Page()
        {
            return View();
        }
        public JsonResult SaveWidgetPosition(List<WidgetBase> widgets)
        {
            WidgetService widgetService = new WidgetService();
            widgets.Each(m =>
            {
                widgetService.Update(m, new Data.DataFilter(new List<string> { "Position" }).Where<WidgetBase>(n => n.ID, OperatorType.Equal, m.ID));
            });
            return Json(true);
        }
    }
}
