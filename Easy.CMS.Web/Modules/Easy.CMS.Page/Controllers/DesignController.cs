using Easy.CMS.Web.Layout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

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
        [ValidateInput(false)]
        public ActionResult SaveLayout(string[] html, LayoutEntity layout)
        {
            LayoutHtmlCollection htmls;
            var zones = Easy.CMS.Web.Zone.Helper.GetZones(html, out htmls);
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
    }
}
